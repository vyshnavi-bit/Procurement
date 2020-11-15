<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CHECKSTATUS.aspx.cs" Inherits="Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        .style10
        {
            font-family: Tahoma;
        }
        .style13
        {
            width: 211px;
            font-family: Tahoma;
            height: 10px;
        }
        .style15
        {
            width: 215px;
            height: 10px;
        }
        .style17
        {
            width: 213px;
            height: 10px;
        }
        .style19
        {
            height: 10px;
        }
        .style20
        {
            width: 216px;
        }
        .style21
        {
            width: 238px;
        }
        .style22
        {
            font-family: Andalus;
            font-size: small;
        }
        .style23
        {
            font-family: Andalus;
        }
        
        
        
        
        .buttonclass
        {
            padding-left: 10px;
            font-weight: bold;
        }
        .buttonclass:hover
        {
            color: white;
            background-color: Orange;
        }
        
        
        .columnscss
        {
            width: 25px;
            font-weight: bold;
            font-family: Verdana;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        .style24
        {
            font-family: Andalus;
            color: #CC0066;
        }
        .style25
        {
            color: #990000;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        .style26
        {
            font-family: Andalus;
            font-size: medium;
        }
    </style>
    <style type="text/css">
        .btn
        {
            background: #a00;
            color: #fff;
            padding: 2px 2px;
            border-radius: 2px;
            -moz-border-radius: 2px;
            -webkit-border-radius: 2px;
            text-decoration: none;
            font: bold 6px Verdana, sans-serif;
            text-shadow: 0 0 1px #000;
            box-shadow: 0 2px 2px #aaa;
            -moz-box-shadow: 0 2px 2px #aaa;
            -webkit-box-shadow: 0 2px 2px #aaa;
            height: 30px;
            width: 85px;
        }
    </style>

    <style type="text/css">
       
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 47%;
            border: 3px solid #0DA9D0;
            padding: 0;
        }
        .modalPopup .header
        {
            background-color: #2FBDF1;
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .body
        {
            min-height: 50px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
            margin-bottom: 5px;
        }
    </style>

    <%--  <script type="text/javascript" >
             window.onload = function () {
                 setInterval('changestate()', 2000);
             };
             var currentState = 'show';
             function changestate() {
                 if (currentState == 'show') {
                     document.getElementById('<%= Button8.ClientID %>').style.display = "none";
                     currentState = 'hide';
                 }
                 else {
                     document.getElementById('<%= Button8.ClientID %>').style.display = "block";
                     currentState = 'show';
                 }
             }
</script>--%>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="background-color: #CCFFFF">
        <ContentTemplate>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <span class="style25"><strong style="font-size: small"><span class="style26">Verify&nbsp;
                    Data</span> </strong></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>
                </strong>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<table class="style3">
                    <div width="100%">
                        <tr align="center">
                            <td align="center" colspan="8">
                                <strong>
                                    <asp:Button ID="Button8" runat="server" BorderStyle="Groove" BorderWidth="1px" CssClass="btn"
                                        Font-Bold="True" Height="30px" OnClick="Button8_Click" Style="font-weight: 700;
                                        font-family: Andalus; font-size: small;" Text="Approval" Width="89px" CausesValidation="False" />
                                </strong>
                            </td>
                        </tr>
                    </div>
                    <tr align="center">
                        <td align="center" colspan="8">
                            <strong>
                                <asp:Label ID="Label18" runat="server" CssClass="style22" Style="font-weight: 700;"
                                    Text="From Date"></asp:Label>
                                <asp:TextBox ID="txt_FromDate" runat="server" CssClass="ddl2" OnTextChanged="txt_FromDate_TextChanged"
                                    Width="125px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" PopupButtonID="txt_FromDate"
                                    PopupPosition="TopRight" TargetControlID="txt_FromDate">
                                </asp:CalendarExtender>
                                <asp:Label ID="Label19" runat="server" CssClass="style22" Style="font-weight: 700;"
                                    Text="To Date"></asp:Label>
                                <asp:TextBox ID="txt_ToDate" runat="server" CssClass="ddl2" OnTextChanged="txt_ToDate_TextChanged"
                                    Width="125px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" PopupButtonID="txt_ToDate"
                                    PopupPosition="TopRight" TargetControlID="txt_ToDate">
                                </asp:CalendarExtender>
                                <asp:Label ID="Label20" runat="server" Style="font-size: small; font-weight: 700;
                                    font-family: Andalus" Text="Plant Name"></asp:Label>
                                <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" Font-Bold="True"
                                    Font-Size="Small" Height="20px" OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged"
                                    Width="120px">
                                </asp:DropDownList>
                                <asp:Button ID="Button7" runat="server" BorderStyle="Groove" BorderWidth="1px" CssClass="buttonclass"
                                    Font-Bold="True" OnClick="Button7_Click" Style="font-weight: 700; font-family: Andalus;
                                    font-size: small;" Text="ALL STATUS" Width="89px" />
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <br />
                        </td>
                    </tr>
                    <tr align="center">
                        <br />
                        <td width="12%">
                            <asp:Label ID="Label6" runat="server" CssClass="style23" Style="font-size: small;
                                font-weight: 700;" Text="Send Rows"></asp:Label>
                            <br />
                            <asp:TextBox ID="TextBox5" runat="server" BorderStyle="Inset" BorderWidth="1px" CssClass="ddl2"
                                Font-Bold="True" Font-Italic="False" Font-Size="Large" ForeColor="Green" Height="25px"
                                Style="font-weight: 700" Width="90px"></asp:TextBox>
                            <br />
                        </td>
                        <td width="12%">
                            <asp:Label ID="Label7" runat="server" CssClass="style23" Style="font-size: small;
                                font-weight: 700;" Text="Approval Rows"></asp:Label>
                            <br />
                            <asp:TextBox ID="TextBox6" runat="server" BorderStyle="Inset" BorderWidth="1px" CssClass="ddl2"
                                Font-Bold="True" Font-Size="Large" ForeColor="Green" Height="25px" Style="font-weight: 700"
                                Width="90px"></asp:TextBox>
                        </td>
                        <td width="12%">
                            <asp:Label ID="Label8" runat="server" CssClass="style23" Style="font-size: small;
                                font-weight: 700;" Text="Total Sessions"></asp:Label>
                            <br />
                            <asp:TextBox ID="TextBox1" runat="server" BorderStyle="Inset" BorderWidth="1px" CssClass="ddl2"
                                Font-Bold="True" Font-Size="Large" ForeColor="Green" Height="25px" Style="font-weight: 700"
                                Width="90px"></asp:TextBox>
                        </td>
                        <td width="12%">
                            <asp:Label ID="Label9" runat="server" CssClass="style23" Style="font-size: small;
                                font-weight: 700;" Text="Total Milkkg"></asp:Label>
                            <br />
                            <asp:TextBox ID="TextBox2" runat="server" BorderStyle="Inset" BorderWidth="1px" CssClass="ddl2"
                                Font-Bold="True" Font-Size="Large" ForeColor="Green" Height="25px" OnTextChanged="TextBox2_TextChanged"
                                Style="font-weight: 700" Width="90px"></asp:TextBox>
                        </td>
                        <td width="12%">
                            <asp:Label ID="Label10" runat="server" CssClass="style23" Style="font-size: small;
                                font-weight: 700;" Text="Avg Fat"></asp:Label>
                            <br />
                            <asp:TextBox ID="TextBox3" runat="server" BorderStyle="Inset" BorderWidth="1px" CssClass="ddl2"
                                Font-Bold="True" Font-Size="Large" ForeColor="Green" Height="25px" Style="font-weight: 700"
                                Width="90px"></asp:TextBox>
                        </td>
                        <td width="12%">
                            <asp:Label ID="Label11" runat="server" CssClass="style23" Style="font-size: small;
                                font-weight: 700;" Text="Avg Snf"></asp:Label>
                            <br />
                            <asp:TextBox ID="TextBox4" runat="server" BorderStyle="Inset" BorderWidth="1px" CssClass="ddl2"
                                Font-Bold="True" Font-Size="Large" ForeColor="Green" Height="25px" Style="font-weight: 700"
                                Width="90px"></asp:TextBox>
                        </td>
                        <td width="12%">
                            <asp:Label ID="Label21" runat="server" CssClass="style23" Style="font-size: small;
                                font-weight: 700;" Text="Total Agents"></asp:Label>
                            <br />
                            <asp:TextBox ID="TextBox7" runat="server" BorderStyle="Inset" BorderWidth="1px" CssClass="ddl2"
                                Font-Bold="True" Font-Size="Large" ForeColor="Green" Height="25px" OnTextChanged="TextBox7_TextChanged"
                                Style="font-weight: 700" Width="90px"></asp:TextBox>
                        </td>
                        <td width="2%">
                            <asp:Label ID="Label22" runat="server" CssClass="style23" Style="font-size: small;
                                font-weight: 700;" Text="Sending Sms"></asp:Label>
                            <br />
                            <asp:TextBox ID="TextBox8" runat="server" BorderStyle="Inset" BorderWidth="1px" CssClass="ddl2"
                                Font-Bold="True" Font-Size="Large" ForeColor="Green" Height="25px" Style="font-weight: 700"
                                Width="90px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="btn_viewdata_click" Text="View Data" />
                        </td>
                    </tr>
                </table>
            </p>
            <p>
                <table class="style3">
                    <tr>
                        <td align="center" class="style13" valign="top">
                        </td>
                        <td align="center" class="style15" valign="top">
                            <span class="style10"><strong>
                                <center>
                                    <span class="style10"><strong>
                                        <asp:Label ID="Label15" runat="server" CssClass="style24" Style="font-size: small;
                                            font-weight: 700;" Text="Remark Status"></asp:Label>
                                        <br />
                                    </strong></span>
                                    <asp:Label ID="Label2" runat="server" CssClass="style24" Style="font-size: small;
                                        font-weight: 700;" Text="Remark Status"></asp:Label>
                                </center>
                                <center>
                                </center>
                                <asp:GridView ID="GridView2" runat="server" AllowPaging="True" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="gridview1"
                                    Font-Size="10px" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="32"
                                    Width="126px">
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
                            </strong></span>
                        </td>
                        <td align="center" class="style17" valign="top">
                            <span class="style10"><strong>
                                <center>
                                    <asp:Label ID="Label16" runat="server" CssClass="style24" Style="font-size: small;
                                        font-weight: 700;" Text="Send Sessions"></asp:Label>
                                    <span align="center" class="style10">
                                        <br />
                                        <asp:Label ID="Label3" runat="server" CssClass="style24" Style="font-size: small;
                                            font-weight: 700;" Text="Send Sessions"></asp:Label>
                                    </span>
                                </center>
                                <asp:GridView ID="GridView3" runat="server" AllowPaging="True" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="gridview1"
                                    Font-Size="10px" OnPageIndexChanging="GridView3_PageIndexChanging" PageSize="32"
                                    Width="126px">
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
                            </strong></span></strong></span>
                        </td>
                        <td valign="top" align="center" class="style19" valign="top">
                            <span class="style10"><strong>
                                <center>
                                    <asp:Label ID="Label17" runat="server" CssClass="style24" Style="font-size: small;
                                        font-weight: 700;" Text="Approval Sessions"></asp:Label>
                                    <br />
                                    <asp:Label ID="Label4" runat="server" CssClass="style24" Style="font-size: small;
                                        font-weight: 700;" Text="Approval Sessions"></asp:Label>
                                </center>
                                <asp:GridView ID="GridView4" runat="server" AllowPaging="True" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="gridview1"
                                    Font-Size="10px" OnPageIndexChanging="GridView4_PageIndexChanging" PageSize="32"
                                    Width="126px">
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
                            </strong></span>
                        </td>
                        <td align="center" class="style13" valign="top">
                            <strong>
                                <br />
                                &nbsp;</strong>
                        </td>
                        <td valign="top" align="center" class="style15">
                            <strong>
                                <asp:Label ID="Label14" runat="server" CssClass="style24" Style="font-size: small;
                                    font-weight: 700;" Text="Zero Values"></asp:Label>
                                <br />
                                <asp:Label ID="Label1" runat="server" CssClass="style24" Style="font-size: small;
                                    font-weight: 700;" Text="Zero Values"></asp:Label>
                                <br />
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="gridview1"
                                    Font-Size="10px" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="32"
                                    Width="126px">
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
                            </strong><span class="style10"><strong>
                                <br />
                                <asp:GridView ID="GridView5" runat="server" Font-Size="10px" OnSelectedIndexChanged="GridView5_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField />
                                    </Columns>
                                </asp:GridView>
                            </strong></span>
                        </td>
                        <td class="style17">
                            <strong>
                                <br />
                            </strong>
                        </td>
                        <td align="center" class="style19">
                            <strong></strong>
                        </td>
                    </tr>
                </table>
                <table class="style3">
                    <tr>
                        <td class="style20">
                            &nbsp;
                        </td>
                        <td class="style21">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style20">
                            &nbsp;
                        </td>
                        <td class="style21">
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
        PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground"
        CancelControlID="btnHide">
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" style="display:none;" >
     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <div class="header">
        Agent Wise Milk Details
        </div>
        <div>
            <asp:GridView ID="grdccdata" runat="server" AllowPaging="true"  OnPageIndexChanging="grdccdata_PageIndexChanging" PageSize="10" CssClass="gridview1" Width="100%">
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
            <asp:Button ID="btnHide" runat="server" Text="Close" OnClick="btnhide_click"/>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <table class="style3">
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="."></asp:Label>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
