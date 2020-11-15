<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MilkStatusAgentWise.aspx.cs" Inherits="MilkStatusAgentWise" %>

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
    <div class="legagentsms">
        <fieldset class="fontt">
            <legend style="color: #3399FF">MilkStatus Agentwise</legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        <asp:Label ID="Label43" runat="server" CssClass="style12" EnableTheming="False" Font-Size="Small"
                            Style="font-family: Andalus; font-size: medium;" Text="Plant Name"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Plantname" runat="server" CssClass="tb10" Font-Size="Small"
                            Height="25px" Width="130px" OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                        &nbsp;
                    </td>
                    <td align="right" class="style13">
                        <asp:Label ID="Label57" runat="server" CssClass="style12" EnableTheming="False" Font-Size="Small"
                            Style="font-family: Andalus; font-size: medium;" Text="Route Name"></asp:Label>
                    </td>
                    <td align="left" class="style13">
                        <asp:DropDownList ID="ddl_RouteName" runat="server" Width="130px" OnSelectedIndexChanged="ddl_RouteName_SelectedIndexChanged"
                            AutoPostBack="True" TabIndex="1" Font-Bold="False" Font-Size="Small" CssClass="tb10"
                            Height="25px">
                        </asp:DropDownList>
                    </td>
                    <td align="left" class="style13">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style13">
                    </td>
                    <td align="right" class="style13">
                        <asp:Label ID="Label55" runat="server" CssClass="style2" Text="From Date"></asp:Label>
                    </td>
                    <td align="left" class="style13">
                        <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" Font-Size="Small" Height="20px"
                            Width="125px"></asp:TextBox>
                        <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="txt_FromDate" PopupPosition="BottomRight" TargetControlID="txt_FromDate">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" class="style13">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label56" runat="server" CssClass="style2" Text="To Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_ToDate" runat="server" CssClass="tb10" Font-Size="Small" Height="20px"
                            Width="125px"></asp:TextBox>
                        <asp:CalendarExtender ID="txt_ToDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                            PopupButtonID="txt_ToDate" PopupPosition="BottomRight" TargetControlID="txt_ToDate">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="3" style="text-align: center">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">AM</asp:ListItem>
                            <asp:ListItem Value="2">PM</asp:ListItem>
                            <asp:ListItem Value="3">DAY</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_RouteID" runat="server" Width="20px" OnSelectedIndexChanged="ddl_RouteID_SelectedIndexChanged"
                            AutoPostBack="True" Visible="false" Height="18px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" Visible="false"
                            Height="16px" Width="29px" OnSelectedIndexChanged="ddl_Plantcode_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" OnClientClick="return validate();"
                            Text="Submit" />
                        &nbsp;<asp:Button ID="Button4" runat="server" CssClass="btn" Font-Bold="True" OnClientClick="return PrintPanel();"
                            Text="Print" OnClick="Button4_Click" />
                        &nbsp;<asp:Button ID="Button3" runat="server" CssClass="btn" OnClick="Button3_Click"
                            Text="Export" />
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td width="12%">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <br />
        </fieldset>
    </div>
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
                            <span class="style4"><strong>AGENT&nbsp; MILK STATUS<br />
                            </strong>
                                <asp:Label ID="lbldis" runat="server" Style="font-weight: 700" Text="Label"></asp:Label>
                            </span>
                        </td>
                    </tr>
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
                                <%--
                     <Columns>
      <asp:TemplateField HeaderText="Sno">
         <ItemTemplate>
               <%# Container.DataItemIndex + 1 %>
         </ItemTemplate>
     </asp:TemplateField>
     </Columns>--%>
                            </asp:GridView>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="Button3" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
