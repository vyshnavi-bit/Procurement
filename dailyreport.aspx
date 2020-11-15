<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="dailyreport.aspx.cs" Inherits="dailyreport" %>

<%@ Register Assembly="Ajaxcontroltoolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        
        
        
        
        body
        {
            font-family: Calibri;
        }
        .gridcss
        {
            background: #df5015;
            font-weight: bold;
            color: White;
        }
        
        
        
        
        
        
        .buttonclass
        {
            padding-left: 10px;
            font-weight: bold;
            height: 26px;
        }
        
        
        
        
        
        .style4
        {
            color: #990033;
        }
        
        
        
        
        
        .style2
        {
            font-family: Andalus;
            font-size: medium;
            color: #000000;
        }
        
        
        
        
        
        .style12
        {
            font-family: Andalus;
            color: #000000;
        }
        
        
        
        
        
        .style13
        {
            height: 31px;
        }
        #table4
        {
            height: 176px;
        }
    </style>
    <script type="text/javascript">
        function Showalert() {
            alert('Call JavaScript function from codebehind');
        }
    </script>
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    <div>
        <fieldset class="fontt">
            <legend style="color: #3399FF">Daily MilkStatus </legend>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" CssClass="style12" EnableTheming="False" Font-Size="Small"
                            Style="font-family: Andalus; font-size: medium;" Text="Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddltype" runat="server" CssClass="tb10" Font-Size="Small" OnSelectedIndexChanged="ddltype_selectedindexchanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="CC Wise" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Routewise" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td id="td1" runat="server">
                        <asp:Label ID="Label43" runat="server" CssClass="style12" EnableTheming="False" Font-Size="Small"
                            Style="font-family: Andalus; font-size: medium;" Text="Plant Name"></asp:Label>
                    </td>
                    <td id="td2" runat="server">
                        <asp:DropDownList ID="ddl_Plantname" runat="server" CssClass="tb10" Font-Size="Small">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label55" runat="server" CssClass="style2" Text="Date"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" Font-Size="Small"></asp:TextBox>
                        <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="txt_FromDate" PopupPosition="BottomRight" TargetControlID="txt_FromDate">
                        </asp:CalendarExtender>
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" OnClientClick="return validate();"
                            Text="Submit" />
                        &nbsp;<asp:Button ID="Button4" runat="server" CssClass="btn" Font-Bold="True" OnClientClick="return PrintPanel();"
                            Text="Print" OnClick="Button4_Click" />
                        &nbsp;<asp:Button ID="Button3" runat="server" CssClass="btn" OnClick="Button3_Click"
                            Text="Export" />
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <br />
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
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlContents" runat="server">
                        <table class="style3">
                            <tr align="center">
                                <td>
                                    <asp:Image ID="Image1" runat="server" Height="75px" ImageUrl="~/Image/VLogo.png"
                                        Width="75px" />
                                    &nbsp;<br />
                                    <span class="style4"><strong>Daily Report
                                    </strong>
                                        <asp:Label ID="lbldis" runat="server" Style="font-weight: 700" Text="Label"></asp:Label>
                                    </span>
                                </td>
                            </tr>
                            <tr><td>A-MORNING COLLECTION</td></tr>
                            <tr align="center">
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" ShowFooter="true" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CausesValidation="false" CellPadding="3"
                                        Font-Size="15px" OnRowCreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                                        PageSize="15">
                                        <RowStyle HorizontalAlign="Right"></RowStyle>
                                        <FooterStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
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
                                    <br />
                                </td>
                            </tr>
                            <tr><td>B-EVENING COLLECTION</td></tr>
                            <tr align="center">
                                <td>
                                    <asp:GridView ID="GridView2" runat="server" ShowFooter="true" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CausesValidation="false" CellPadding="3" OnRowDataBound="GridView2_RowDataBound"
                                        Font-Size="15px"
                                        PageSize="15">
                                        <RowStyle HorizontalAlign="Right"></RowStyle>
                                        <FooterStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
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
                                    <br />
                                </td>
                            </tr>
                            <tr align="center"><td>C-Day Total</td></tr>
                            <tr align="center">
                                <td>
                                <table>
                                <tr><td>
                                    <asp:GridView ID="GridView3" runat="server" ShowFooter="true" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CausesValidation="false" CellPadding="3" OnRowDataBound="GridView3_RowDataBound"
                                        Font-Size="15px"
                                        PageSize="15">
                                        <RowStyle HorizontalAlign="Right"></RowStyle>
                                        
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
                                    <br />
                               </td>
                              <td>
                                    <asp:GridView ID="GridView4" runat="server" ShowFooter="true" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CausesValidation="false" CellPadding="3" 
                                        Font-Size="15px"
                                        PageSize="15">
                                        <RowStyle HorizontalAlign="Right"></RowStyle>
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
                                    <br />
                                    </td></tr></table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="Button3" />
                </Triggers>
            </asp:UpdatePanel>
        </fieldset>
    </div>
</asp:Content>
