<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_ChartRoute.aspx.cs" Inherits="Frm_ChartRoute" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="JavaScript" src="FusionCharts/FusionCharts.js"></script>
      <script type="text/javascript" language="JavaScript">

          function myJS(myVar) {
              window.alert(myVar);
          }
      
      </script>
      <link id="Link1"  rel="Stylesheet" type="text/css" href="SampleStyleSheet1.css" runat="server" media="screen" />
    <style type="text/css">
        .style1
        {
            height: 9px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    <table width="650px" border="0" cellspacing="0" cellpadding="3" align="center">
    <tr>
            <td valign="top" class="text" align="center" >
           <table width="650px" border="0" cellspacing="0" cellpadding="3" align="center">
           <tr>
           <td>
           <fieldset class="fontt">
           <legend class="fontt">Graphical Charts</legend>
           <table width="600px" border="0" cellspacing="0" cellpadding="3" align="center">
           <tr>
           
            <td height="20px" width="175px" valign="middle" align="center">
               <asp:Label ID="Label1" runat="server" Text="ChartType"></asp:Label>
           </td>
            <td height="20px" width="175px" valign="middle" align="left">
                <asp:DropDownList ID="ddl_charttype" runat="server" Width="175px" Height="25px">
                    <asp:ListItem Value="1">Bargraph</asp:ListItem>
                    <asp:ListItem Value="2">piegraph</asp:ListItem>
                    <asp:ListItem Value="3">Linegraph</asp:ListItem>
                    <asp:ListItem Value="4">Doughnutgraph</asp:ListItem>
                </asp:DropDownList>
           </td>
            <td height="20px" width="250px" valign="middle" align="left">   
             <asp:Label ID="Label4" runat="server" Text="From"></asp:Label> 
                <asp:TextBox ID="txt_frmdate" runat="server" Height="16px" Width="80px" 
                    ></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_frmdate"
                    PopupButtonID="txt_frmdate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                </asp:CalendarExtender>              
            <asp:Label ID="Label5" runat="server" Text="To"></asp:Label>  
                   <asp:TextBox ID="txt_todate" runat="server" Height="17px" Width="79px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_todate"
                    PopupButtonID="txt_todate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                </asp:CalendarExtender>                   
           </td>
           </tr>
           <tr>
           
            <td height="20px" width="175px" valign="middle" align="center">
                <asp:Label ID="Lbl_plantname" runat="server" Text="PlantName"></asp:Label>
                </td>
           <td height="20px" width="175px" valign="middle" align="left"> 
                  
            <asp:DropDownList ID="ddl_Plantname" runat="server" Width="175px" Height="25px" 
                   onselectedindexchanged="ddl_plantname_SelectedIndexChanged">
                </asp:DropDownList>               
                  
           </td>
            <td height="20px" width="250px" valign="middle" align="left"> 
            <table border="1">
            <tr>
            <td class="style1">
             <asp:RadioButton ID="rd_route" runat="server" Text="Route" AutoPostBack="True" 
                    Checked="True" oncheckedchanged="rd_route_CheckedChanged"  />
             <asp:RadioButton ID="rd_plant" runat="server" Text="Plant" AutoPostBack="True" 
                    oncheckedchanged="rd_plant_CheckedChanged" />
            </td>
            </tr>
             
            </table>                    
             </td>
           </tr>
           <tr>
           
            <td width="175px" valign="middle" align="center">
                &nbsp; 
                <asp:Label ID="Label3" runat="server" Text="Select Report"></asp:Label>
                </td>
           <td width="175px" valign="middle" align="left"> 
             <asp:DropDownList ID="ddl_reportname" runat="server" Width="175px" Height="25px" 
                   onselectedindexchanged="ddl_reportname_SelectedIndexChanged">
                 <asp:ListItem Value="1">MilkSummary</asp:ListItem>
                 <asp:ListItem Value="2">AgentcountSummary</asp:ListItem>
                 <asp:ListItem Value="3">Remarks Summary</asp:ListItem>
                </asp:DropDownList>                   
           </td>
            <td width="250px" valign="middle" align="left">                
            <asp:DropDownList ID="ddl_Plantcode" runat="server" Width="40px" Height="19px" 
                    Visible="False">
                </asp:DropDownList>               
        <asp:Button ID="btn_Report" runat="server" onclick="btn_Report_Click" 
            Text="GraphReport " BackColor="#6F696F" ForeColor="White" 
            Width="84px" ToolTip="Daily Report"  />
   
               </td>
           </tr>
         
           </table>
           </fieldset>
           </td>
           </tr>
           </table>
	        </td> 
          </tr>
          <tr>
            <td valign="top" class="text" align="center" >
            <asp:Literal ID="FCLiteral1" runat="server"></asp:Literal>
	        </td> 
          </tr>
          <tr> 
            <td valign="top" class="text" align="center">
            <asp:Literal ID="FCLiteral3" runat="server"></asp:Literal>
	        </td>
          </tr>          
        </table>    
</asp:Content>


