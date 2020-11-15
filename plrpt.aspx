<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="plrpt.aspx.cs" Inherits="plrpt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 1%;
        }
        .style2
        {
            float: right;
            width: 29%;
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
                            &nbsp;&nbsp;Total Abstract
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
            <div style="width: 100%;">
                <table width="100%">
                    <tr style="display: none;">
                        <td width="10%">
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="rbt_openingstock" Format="MM/dd/yyyy" PopupPosition="TopRight">
                            </asp:CalendarExtender>
                        </td>
                        <td width="12%">
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="rpt_closingstock" Format="MM/dd/yyyy" PopupPosition="TopRight">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="fontt">
                            From Date:
                        </td>
                        <td class="fontt" align="right">
                            <asp:TextBox ID="txt_FromDate" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="MM/dd/yyyy" PopupPosition="TopRight">
                            </asp:CalendarExtender>
                        </td>
                        <td class="fontt">
                            To Date:
                        </td>
                        <td class="fontt" align="right">
                            <asp:TextBox ID="txt_ToDate" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="MM/dd/yyyy" PopupPosition="TopRight">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:Label ID="lbl_PlantName" runat="server" Text="PlantName"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_PlantName" runat="server" Width="120px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" BackColor="#6F696F"
                                ForeColor="White" Text="Plant Wise" Width="100px" />
                        </td>
                        <td>
                          <a href="exporttoxl.aspx">Export to XL</a>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lbl_PlantID" runat="server" Text="Plant ID" Visible="False"></asp:Label>
                        </td>
                        <td width="25%">
                            <asp:DropDownList ID="ddl_PlantID" runat="server" Width="120px" AutoPostBack="True"
                                Enabled="False" Visible="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="hidepanel" runat="server" Visible='false'>
                <div id="divPrint">
                    <div style="width: 100%;">
                        <div>
                            <asp:GridView ID="grdReports" Width="100%" runat="server" CssClass="gridcls" Font-Bold="true"
                                Font-Size="Smaller" ForeColor="White" GridLines="Both" OnRowDataBound="grdReports_RowDataBound">
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                <HeaderStyle BackColor="#f4f4f4" Font-Bold="False" Font-Italic="False" Font-Names="Raavi"
                                    ForeColor="Black" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                <AlternatingRowStyle HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <br />
                <br />
            </asp:Panel>
        </ContentTemplate>
       
    </asp:UpdatePanel>
</asp:Content>
