<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TruckRouteAllotment.aspx.cs" Inherits="TruckRouteAllotment" Title="OnlineMilkTest|TruckRouteAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="Stylesheet" />
    <style type="text/css">
        .cellwid
        {
            width: 8px;
        }
        .panels
        {
            width: 100px;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
        .fontfam
        {
            font-family: Arial;
        }
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
        
        .style1
        {
            width: 100%;
        }
        
        .style11
        {
            width: 30%;
        }
    </style>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            //       printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 100);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..."
                    ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 38%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style1">
                <tr>
                    <td>
                        <center>
                            <div align="center">
                                <fieldset class="style11">
                                    <legend class="fontt">TRUCK ROUTE ALLOTMENT</legend>
                                    <table id="table12" border="0" cellspacing="1" width="100%">
                                        <tr valign="top">
                                            <td align="right">
                                                <asp:Label ID="lbl_PlantName" runat="server" Font-Bold="true" Font-Size="12px" Text="PlantName"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" CssClass="ddlText"
                                                    Font-Bold="True" Font-Size="Medium" OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged"
                                                    Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td align="right">
                                                <asp:Label ID="lbl_VehicleName" runat="server" Font-Bold="true" Font-Size="12px"
                                                    Text="VehicleName"></asp:Label>
                                            </td>
                                            <td style="margin-left: 40px">
                                                <asp:DropDownList ID="ddl_VehicleName" runat="server" AutoPostBack="true" Font-Bold="true"
                                                    Font-Size="Medium" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td align="right">
                                                <asp:Label ID="lbl_Distance" runat="server" Font-Bold="true" Font-Size="12px" Text="Distance[KM]"></asp:Label>
                                            </td>
                                            <td style="margin-left: 40px">
                                                <asp:TextBox ID="txt_TotDistance" runat="server" CssClass="DText" Font-Bold="true"
                                                    Font-Size="20px" ForeColor="Green" Width="60px"> </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td align="left">
                                            </td>
                                            <td align="left">
                                                <asp:Button ID="Button1" runat="server" BackColor="Green" BorderStyle="Double" Font-Bold="True"
                                                    ForeColor="White" Height="26px" OnClick="Button1_Click" OnClientClick="return confirm('Do U want to Save?');"
                                                    Text="SAVE" />
                                                <asp:Button ID="btn_Reset" runat="server" BackColor="Red" BorderStyle="Double" Font-Bold="True"
                                                    ForeColor="White" Height="26px" OnClick="btn_Reset_Click" OnClientClick="return confirm('Are You Reset the Saved Data?');"
                                                    Text="Reset" />
                                                <asp:Button ID="btn_print" runat="server" BackColor="#00CCFF" BorderStyle="Double"
                                                    Font-Bold="True" ForeColor="White" Height="26px" OnClientClick="return PrintPanel();"
                                                    Text="Print" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </center>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td valign="top" align="CENTER" style="width: 75%; text-align: center">
                        <fieldset class="fontt" align="CENTER">
                            <center>
                                <table class="style1">
                                    <tr valign="top" align="center">
                                        <td width="50%" align="right">
                                            <asp:Table ID="Table2" runat="Server" BorderColor="White" BorderWidth="1" CaptionAlign="Top"
                                                CellPadding="1" CellSpacing="1" Height="40px" Style="margin-left: 0px">
                                                <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1">
                                                    <asp:TableCell ID="TableCell22" runat="Server" BorderWidth="1">
                                                        <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" CellPadding="1"
                                                            CellSpacing="1" Width="300px" CaptionAlign="Top" Height="40px">
                                                            <asp:TableRow ID="Title_TableRow" runat="Server" BorderWidth="1" BackColor="#3399CC"
                                                                ForeColor="white" BorderColor="Silver">
                                                                <asp:TableCell ID="TableCell6" runat="Server" BorderWidth="2">
                                                                    <asp:CheckBox ID="MChk_RouteName" Text="Route_Name" runat="server" AutoPostBack="True"
                                                                        OnCheckedChanged="MChk_RouteName_CheckedChanged" />
                                                                </asp:TableCell>
                                                                <asp:TableCell ID="TableCell7" runat="Server" BorderWidth="2">
                                                                    <asp:CheckBox ID="MChk_RouteDistance" Text="R_Distance_ID" runat="server" Enabled="false" />
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow ID="TableRow2" runat="Server" BorderWidth="1" BorderColor="Silver"
                                                                BackColor="#ffffec">
                                                                <asp:TableCell ID="TableCell1" runat="Server" BorderWidth="1">
                                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderWidth="1">
                                                                    </asp:CheckBoxList>
                                                                </asp:TableCell>
                                                                <asp:TableCell ID="TableCell2" runat="Server" BorderWidth="1">
                                                                    <asp:CheckBoxList ID="CheckBoxList2" runat="server" BorderWidth="1" Enabled="false">
                                                                    </asp:CheckBoxList>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </td>
                                        <td align="left">
                                            <asp:GridView ID="AgentwisePaymentDetails" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="12px">
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <RowStyle ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </fieldset>
                        <asp:Panel ID="pnlContents" runat="server">
                            <center>
                                <asp:Label ID="Lbl_Errormsg" runat="server" Font-Size="Large"></asp:Label>
                            </center>
                        </asp:Panel>
                </tr>
            </table>
            <center>
                <br />
                <br />
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
