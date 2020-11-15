<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SelectRatechart.aspx.cs" Inherits="SelectRatechart" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="Stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 4%;
        }
        .style2
        {
            width: 4%;
            height: 43px;
        }
        .style3
        {
            height: 43px;
            text-align: left;
        }
        .style4
        {
            height: 43px;
            text-align: left;
        }
        .style5
        {
            width: 19%;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;SELECT RATE CHART</p>
            </td>
        </tr>
        <tr>
            <td width="100%" class="line" height="1px">
            </td>
        </tr>
        </table>

    </br>
<div class="legselectratechart">
<fieldset class="fontt">
<legend class="fontt">SELECT RATE CAHRT</legend>
<table border="0" width="100%" id="table12" cellspacing="1">
           <tr>
                    <td class="style2">
                        &nbsp;</td>
                   <td class="style4" align="center">
                       <asp:RadioButtonList ID="Rate_type" runat="server" RepeatDirection="Horizontal">
                           <asp:ListItem Value="1">PlantWiseRatechart</asp:ListItem>
                           <asp:ListItem Value="2">RouteWiseRate</asp:ListItem>
                       </asp:RadioButtonList>
    </td>
                   <td class="style4" align="center">
        <asp:Button ID="btn_Ok" runat="server" Text="Ok" onclick="btn_Ok_Click" 
        Width="70px" BackColor="#6F696F"   ForeColor="White" TabIndex="4"/>
    </td>
        </tr>
           <tr>
                    <td class="style2">
                    </td>
                   <td class="style4" align="center">
    <asp:Label ID="lbl_Plantname" runat="server" Text="Plantname : " Visible="False"></asp:Label>
    </td>
    <td width="25%" class="style3">
<asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
        Width="170px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
            Visible="False"></asp:DropDownList>
        </td>
        </tr>
        <tr>
   <td class="style1">
                    </td>
                   <td class="style5">   
<asp:RadioButton runat="server"  ID="rd_Plantwiseratechart" AutoPostBack="true" 
        Text="PlantwiseRatechart" Checked="True" 
        oncheckedchanged="rd_Plantwiseratechart_CheckedChanged" TabIndex="1" Visible="False"/>
        </td>

  <td width="25%">   
<asp:RadioButton runat="server" ID="rd_Routewiseratechart" AutoPostBack="true" 
        Text="RoutewiseRatechart" 
        oncheckedchanged="rd_Routewiseratechart_CheckedChanged" TabIndex="2" 
          Visible="False"  />
       </td> </tr>
       <tr>
       <td class="style1">  
         
        </td>
       </tr>
        </table>

        <center>
                        <asp:Button ID="btn_SaveData" runat="server" Text="Save" 
        Width="70px" onclick="btn_SaveData_Click" BackColor="#6F696F" ForeColor="White" TabIndex="3" 
                            Visible="False" />
        &nbsp;<br />
                  </center>
</fieldset>
</div>
<br />
<br />

<%--<uc1:uscMsgBox ID="uscMsgBox1" runat="server" />--%>
</asp:Content>
