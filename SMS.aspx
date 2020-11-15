<%@ Page Title="Online Milk Test|SMS" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="SMS.aspx.cs" Inherits="SMS" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .stylesfiledset
        {
            width: 40%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdateProgress ID="updProgress"  runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: #FFFFFF; opacity: 0.7;">
                <br />
                <div align="center" class="legendloadimg">
                    <img alt="progress" src="Image/loadingimage.gif" style="padding: 10px; position: absolute;
                        top: 35%; left: 40%;" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                <tr>
                    <td width="100%">
                        <br />
                        <p class="subheading" style="line-height: 150%">
                            &nbsp;&nbsp;SMS
                        </p>
                    </td>
                </tr>
                <tr>
                    <td width="100%" height="3px">
                    </td>
                </tr>
                <tr>
                    <td width="40%" class="line" height="1px">
                    </td>
                </tr>
                <tr>
                    <td width="40%" height="1px" class="line">
                        &nbsp;
                    </td>
                </tr>
                <tr width="40%" align="center">
                    <td width="40%" height="7">
                        <fieldset class="stylesfiledset">
                            <legend class="fontt">SMS</legend>
                            <br />
                            <br />
                            <table id="table4" align="center" border="0" cellspacing="1" width="100%">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" Height="16px"
                                            Visible="false" Width="29px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        Plant Name :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged"
                                            Width="170px">
                                        </asp:DropDownList>
                                        <asp:Button ID="btn_smsstsus" runat="server" BackColor="#00CC00" BorderStyle="Double"
                                            Font-Bold="True" ForeColor="White" Height="26px" OnClick="btn_smsstsus_Click"
                                            Text="Sms Status" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="right">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="right">
                                        Sms Send Date:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_FromDate" runat="server" AutoPostBack="True"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txt_FromDate"
                                            PopupPosition="TopRight" TargetControlID="txt_FromDate">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txt_ToDate" runat="server" AutoPostBack="True" Height="16px" TabIndex="1"
                                            Visible="False" Width="24px"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupButtonID="txt_ToDate"
                                            PopupPosition="TopRight" TargetControlID="txt_ToDate">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="Chk_Rate" runat="server" Enabled="False" Text="Without_Rate" />
                                        <br />
                                        Transfered Sms :
                                        <asp:TextBox ID="txt_currentsms" runat="server" AutoPostBack="True" Font-Bold="True"
                                            Font-Size="X-Large" ForeColor="Red" Height="38px" Style="margin-bottom: 0px"
                                            Width="42px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <center>
                                <asp:Button ID="Button1" runat="server" BackColor="#6F696F" ForeColor="White" OnClick="Button1_Click"
                                    Style="text-align: justify" TabIndex="2" Text="Send Sms" Width="82px" />
                                <br />
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="79px"
                                    PageSize="3" Style="text-align: center">
                                    <RowStyle ForeColor="#000066" />
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </center>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="TextBox3" runat="server" Visible="False"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
