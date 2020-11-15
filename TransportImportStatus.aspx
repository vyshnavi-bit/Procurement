<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TransportImportStatus.aspx.cs" Inherits="TransportImportStatus" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 9pt;
        }
        .Grid th
        {
            color: #fff;
            background-color: #3AC0F2;
        }
        /* CSS to change the GridLines color */
        .Grid, .Grid th, .Grid td
        {
            border: 1px solid #525252;
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
        .style1
        {
            font-family: Andalus;
            font-size: medium;
            color: #000000;
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
                    ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <center>
            <table width=40%>
                <tr>
                    <td>
                        <center>
                            <fieldset class="fontt">
                                <legend style="color: #3399FF">TransportImportStatus Report</legend>
                                <table ID="table4" align="center" border="0" cellspacing="1" width="100%">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="right">
                                            &nbsp;<asp:Label ID="Label2" runat="server" CssClass="style1" Text="From"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txt_FromDate" runat="server" CssClass="DText"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                                PopupButtonID="txt_FromDate" PopupPosition="BottomRight" 
                                                TargetControlID="txt_FromDate">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label3" runat="server" CssClass="style1" Text="To"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txt_ToDate" runat="server" CssClass="DText"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                                                PopupButtonID="txt_ToDate" PopupPosition="BottomRight" 
                                                TargetControlID="txt_ToDate">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btn_ok" runat="server" BackColor="Green" BorderStyle="Double" 
                                                Font-Bold="True" ForeColor="White" Height="30px" OnClick="btn_ok_Click" 
                                                Text="Generate" />
                                            <asp:Button ID="btn_print" runat="server" BackColor="#00CCFF" 
                                                BorderStyle="Double" Font-Bold="True" Font-Size="Small" ForeColor="White" 
                                                Height="30px" OnClientClick="return PrintPanel();" Text="Print" />
                                        </td>
                                        <td width="12%">
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="Lbl_Errormsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                            </fieldset>
                        </center>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        
                            <fieldset class="fontt">
                                <legend style="color: #3399FF">Plant List</legend>
                                <asp:Panel ID="Panel3" runat="server">
                                    <asp:Table ID="Table2" runat="Server" BorderColor="White" BorderWidth="1" 
                                        CaptionAlign="Top" CellPadding="1" CellSpacing="1" Height="40px" 
                                        Style="margin-left: 0px" Width="200px">
                                        <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1" Width="250px">
                                            <asp:TableCell ID="TableCell22" runat="Server" BorderWidth="1">
                                            <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" 
                                                CaptionAlign="Top" CellPadding="1" CellSpacing="1" Height="40px" Width="250px">
                                                <asp:TableRow ID="Title_TableRow" runat="Server" BackColor="#3399CC" 
                                                    BorderColor="Silver" BorderWidth="1" ForeColor="white">
                                                    <asp:TableCell ID="TableCell6" runat="Server" BorderWidth="0">
                                                    <asp:CheckBox ID="MChk_PlantName" runat="server" AutoPostBack="True" 
                                                        Checked="true" OnCheckedChanged="MChk_PlantName_CheckedChanged" 
                                                        Text="PlantName" />
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="TableRow2" runat="Server" BackColor="#ffffec" 
                                                    BorderColor="Silver" BorderWidth="0">
                                                    <asp:TableCell ID="TableCell1" runat="Server" BorderWidth="0">
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderWidth="0">
                                                    </asp:CheckBoxList>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:Panel>
                            </fieldset>
                        
                    </td>
                </tr>
            </table>
                 </center>
            <br />
            <br />

            <td style='text-align: left; vertical-align: top'>
            </td>
            <div align="center">
                <asp:Panel ID="pnlContents" runat="server">
                    <div align="center">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/VLogo.png" Width="100px"
                            Height="50px" />
                    </div>
                   <center> <asp:Label ID="Label1" runat="server" Text="Transport Import Status" 
                   style="font-family: Arial; font-size: medium; background:green; color:White;font-weight:bold "></asp:Label>  </center>
                    <div align="center">
                        <asp:GridView ID="Transport_ImportStatus" runat="server" BackColor="White" AllowPaging="True"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="12px"
                            OnPageIndexChanging="Transport_ImportStatus_PageIndexChanging">
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
                        <br />
                    </div>
                </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
