<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TransportRpt.aspx.cs" Inherits="TransportRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .DText
        {
            width: 150px;
            height: 20px;
            border-radius: 5px;
            border: 1px solid #CCC;
            outline: none;
            border-color: #9ecaed;
            box-shadow: 0 0 10px #9ecaed;
            padding: 8px;
            font-weight: 200;
            font-size: 15px;
            font-family: Verdana;
        }
        .stylefiledset
        {
           width:40%; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                <tr>
                    <td width="100%" colspan="2">
                        <br />
                        <p class="subheading" style="line-height: 150%">
                            &nbsp;&nbsp;TRANSPORT DETAILS
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <center>
    <div align=center class=stylefiledset>
        <fieldset class="fontt">
            <legend style="color: #3399FF">Transport Report</legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">
                <tr>
                    <td align="right">
                        <asp:Label ID="Label1" runat="server" Text="Plant_Name"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" Width="200px"
                            OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged" Font-Bold="True"
                            Font-Size="Large">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;<asp:Label ID="Label2" runat="server" Text="From"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_FromDate" runat="server" CssClass="DText"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                            PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="BottomRight">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_ToDate" runat="server" CssClass="DText" TabIndex="1"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_ToDate"
                            PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="BottomRight">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" Visible="false"
                            Height="16px" Width="29px">
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" BackColor="Green"
                            BorderStyle="Double" Font-Bold="True" ForeColor="White" Text="Generate" Width="70px"
                            Height="35px" TabIndex="2" />
                        <asp:Button ID="btn_Export" runat="server" BackColor="#00CCFF" 
                BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="35px" onclick="btn_Export_Click" Text="Export" 
                            Width="70px"  />
                    </td>
                    <td width="12%">
                       
                    </td>
                </tr>
            </table>
            <br />
        </fieldset>
    </div>
    </center>
    </table>
    <table width=100%>
    <tr>
    <td align=center >
      <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" 
        ToolPanelView="None" onunload="CrystalReportViewer1_Unload" 
            oninit="CrystalReportViewer1_Init" />
        </td>
    </tr>
    </table>
</asp:Content>
