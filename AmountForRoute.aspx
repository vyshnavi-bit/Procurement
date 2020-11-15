<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AmountForRoute.aspx.cs" Inherits="AmountForRoute" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style7
        {
            border-right: 1px solid #3366FF;
            border-top: 1px solid #3366FF;
            border-bottom: 1px solid #3366FF;
            border-left: 4px solid #3366FF;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    <div align="left">
        <asp:HyperLink ID="HyperLink1" CssClass="fontt" runat="server" NavigateUrl="~/AMOUNTALLOTEMENTSaspx.aspx">Admin Amount Allot</asp:HyperLink>
    </div>
    <div class="legagentsms">
        <fieldset class="fontt">
            <legend style="color: #3399FF">Plant Amount Allotment </legend>
            <table id="table4" align="center" border="0" cellspacing="1" width="100%">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Label ID="Label10" runat="server" Text="Ref_No"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_tid" runat="server" Width="130px" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label11" runat="server" Text="User ID/Name"></asp:Label>
                    </td>
                    <td align="right">
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_userid" runat="server" Width="130px" OnTextChanged="txt_userid_TextChanged"
                            Enabled="False"></asp:TextBox>
                        <asp:TextBox ID="txt_name" runat="server" Width="100px" Enabled="False" Visible="False">Accounts</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label12" runat="server" Text="Date/Time"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_FromDate" runat="server" CssClass="style7" Height="24px" OnTextChanged="txt_FromDate_TextChanged"
                            Width="130px" Enabled="False"></asp:TextBox>
                        <asp:TextBox ID="txt_time" runat="server" Width="100px" Enabled="False" Height="20px"
                            Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label4" runat="server" Text="Avail_Balance"></asp:Label>
                    </td>
                    <td align="right">
                    </td>
                    <td align="left">
                        <asp:TextBox ID="Txt_Availamount" runat="server" CssClass="tb8" Height="30px" OnTextChanged="txt_FromDate_TextChanged"
                            Width="130px" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label14" runat="server" Text="Description"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_description" runat="server" Height="77px" Width="243px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td width="12%">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                    </td>
                    <td align="left">
                        <asp:Button ID="btn_Check" runat="server" BackColor="#6F696F" ForeColor="White" Text="Check"
                            Width="70px" Height="26px" OnClick="btn_Check_Click" />
                        <asp:Button ID="btn_save" runat="server" BackColor="#6F696F" ForeColor="White" Height="26px"
                            Text="Save" Width="70px" OnClick="btn_save_Click" />
                    </td>
                </tr>
            </table>
            <br />
        </fieldset>
    </div>
    <br />
    <div class="AmountForRoute">
        <fieldset class="fontt">
            <legend style="color: #3399FF">Plant List</legend>
            <asp:Panel ID="Panel3" runat="server">
                <asp:Table ID="Table2" runat="Server" BorderColor="White" BorderWidth="1" CaptionAlign="Top"
                    CellPadding="1" CellSpacing="1" Height="40px" Style="margin-left: 0px" Width="650px">
                    <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1" Width="650px">
                        <asp:TableCell ID="TableCell22" runat="Server" BorderWidth="1">
                            <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" CellPadding="1"
                                CellSpacing="1" Width="160px" CaptionAlign="Top" Height="40px">
                                <asp:TableRow ID="Title_TableRow" runat="Server" BorderWidth="1" BackColor="#3399CC"
                                    ForeColor="white" BorderColor="Silver">
                                    <asp:TableCell ID="TableCell7" runat="Server" BorderWidth="0">
                                        <div align="center">
                                            <asp:Label ID="Label2" runat="server" Text="PaymentPeriod" Width="200"></asp:Label>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell6" runat="Server" BorderWidth="0">
                                        <asp:CheckBox ID="MChk_PlantName" Text="PlantName" runat="server" Checked="true"
                                            Width="130" Enabled="false" AutoPostBack="True" OnCheckedChanged="MChk_PlantName_CheckedChanged" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell5" runat="Server" BorderWidth="0">
                                        <div align="center">
                                            <asp:Label ID="Label1" runat="server" Text="BalanceAmount"></asp:Label>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell2" runat="Server" BorderWidth="0">
                                        <div align="center">
                                            <asp:Label ID="Label3" runat="server" Text="Amount" Width="100"></asp:Label>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow2" runat="Server" BorderWidth="0" BorderColor="Silver"
                                    BackColor="#ffffec">
                                    <asp:TableCell ID="TableCell8" runat="Server" BorderWidth="0">
                                        <asp:CheckBoxList ID="CheckBoxList3" runat="server" Width="200" Height="40px" BorderWidth="0"
                                            Enabled="false">
                                        </asp:CheckBoxList>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell1" runat="Server" BorderWidth="0">
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" Width="130px" Height="40px" BorderWidth="0"
                                            Enabled="false">
                                        </asp:CheckBoxList>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell4" runat="Server" BorderWidth="0">
                                        <asp:CheckBoxList ID="CheckBoxList2" runat="server" Width="130px" Height="40px" BorderWidth="0"
                                            Enabled="false">
                                        </asp:CheckBoxList>
                                    </asp:TableCell>
                                    <asp:TableCell ID="TableCell3" runat="Server" BorderWidth="0">
                                        <table class="style1">
                                            <tr>
                                                <td width="10%">
                                                    <fieldset>
                                                        <table class="style1">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_arani" runat="server" Width="130px" placeholder="ARANI" Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_kaveri" runat="server" Width="130px" placeholder="KAVERI" Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_gud" runat="server" Width="130px" placeholder="GUDLUR" Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_wal" runat="server" Width="130px" placeholder="WALAJA" Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_vkota" runat="server" Width="130px" placeholder="VKOTA" Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_palli" runat="server" Width="130px" placeholder="PALLIPATTU"
                                                                        Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_rcpura" runat="server" Width="130px" placeholder="RCPURAM" Font-Size="12px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_bomm" runat="server" Width="130px" placeholder="BOMMA" Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_tari" runat="server" Width="130px" placeholder="TARIGONDA" Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_kala" runat="server" Width="130px" placeholder="KALAHASTI" Font-Size="12px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_cspur" runat="server" Width="130px" placeholder="CSPURAM" Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_kon" runat="server" Width="130px" placeholder="KONDEPI" Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_kava" runat="server" Width="130px" placeholder="KAVALI" Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_Gudipalli" runat="server" Width="130px" placeholder="GUDIPALLIPADU"
                                                                        Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_kaligiri" runat="server" Width="130px" placeholder="KALIGIRI"
                                                                        Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_gudicow" runat="server" Width="130px" placeholder="GUDIPALLIPADU COW"
                                                                        Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_kdpcow" runat="server" Width="130px" placeholder="Kondepi COW"
                                                                        Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_allapati" runat="server" Width="130px" placeholder="Allapati"
                                                                        Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
