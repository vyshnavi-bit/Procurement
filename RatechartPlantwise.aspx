<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RatechartPlantwise.aspx.cs" Inherits="RatechartPlantwise" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="cr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%" colspan="2">
                <br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;PLANTWISE RATECHART
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
    <div class="legPlantratechart">
        <fieldset class="fontt">
            <legend>PLANTWISE RATECHART</legend>
            <table border="0" width="100%" id="table2" cellspacing="1">
                <tr>
                    <td width="20%">
                    </td>
                    <td width="15%">
                        <asp:Label ID="lbl_Plantname" runat="server" Text="Plantname"></asp:Label>
                    </td>
                    <td width="25%">
                        <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged"
                            Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td width="20%">
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <table border="0" width="100%" id="table3" cellspacing="1">
                <tr>
                    <td width="18%">
                    </td>
                    <td width="10%" class="fontt">
                        <asp:Label ID="lbl_CowRatechart" runat="server" Text="CowRatechart"></asp:Label>
                    </td>
                    <td width="15%" class="fontt" align="right">
                        <asp:DropDownList ID="ddl_Ratechart" runat="server" AutoPostBack="true" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td width="10%">
                    </td>
                    <td width="9%" class="fontt">
                        <asp:Label ID="lbl_BuffaloRatechart" runat="server" Text="BuffaloRatechart"></asp:Label>
                    </td>
                    <td width="15%" class="fontt" align="right">
                        <asp:DropDownList ID="ddl_RatechartBuff" runat="server" AutoPostBack="true" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td width="20%">
                    </td>
                </tr>
            </table>
            <table border="0" width="100%" id="table1" cellspacing="1">
                <tr>
                    <td width="20%">
                    </td>
                    <td width="30%">
                    </td>
                    <td width="20%" align="right">
                        <asp:Button ID="btn_PlantSave" Text="SAVE" Height="25px" BackColor="#6F696F" ForeColor="White"
                            runat="server" OnClientClick="return Confirm(Are You Sure Save the PlantwiseRatechart)"
                            OnClick="btn_PlantSave_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <br />
    <cr:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
    <br />
    <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
</asp:Content>
