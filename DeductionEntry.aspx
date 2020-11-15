<%@ Page Title="OnlineMilkTest|DeductionLoan" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="DeductionEntry.aspx.cs" Inherits="DeductionEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />
    <script type="text/javascript" language="ecmascript" src="djs.js"></script>
    <style type="text/css">
        .style1
        {
            height: 30px;
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
        
        
        
        
        .style2
        {
            width: 100%;
        }
        .style3
        {
            font-family: Andalus;
            font-size: small;
            font-weight: normal;
            font-style: normal;
            color: #CC0066;
        }
        .style4
        {
            color: #CC0066;
            font-family: Andalus;
        }
        
        
        
        .style5
        {
            color: #CC0066;
            font-family: Andalus;
            font-size: medium;
        }
        
        .style55
        {
            width: 60%;
        }
        
        
        
        .style6
        {
            font-family: Andalus;
            font-size: large;
            color: #660066;
        }
        .style7
        {
            font-size: medium;
        }
        
        
        
        .style56
        {
            font-size: medium;
            font-family: Andalus;
            color: #800000;
        }
        
        
        .ddl2
        {
            border: 2px solid #7d6754;
            border-radius: 5px;
            padding: 3px;
            -webkit-appearance: none;
            background-image: url('Images/Arrowhead-Down-01.png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: '';
            font-size: small;
            font-family: Andalus;
            color: #800000;
            font-weight: 700;
        }
        
        
        
        .style57
        {
            width: 3%;
        }
        .style58
        {
            height: 30px;
            width: 3%;
        }
        .buttonclass
        {
            padding-left: 10px;
            font-weight: bold;
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
        
        
        
        
        
        
        
        
        
        
        
        
        #table2
        {
            height: 58px;
            width: 107%;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        .table-hover
        {
            margin-top: 0px;
        }
    </style>
    <%--<script type="text/javascript" >
             window.onload = function () {
                 setInterval('changestate()', 2000);
             };
             var currentState = 'show';
             function changestate() {
                 if (currentState == 'show') {
                     document.getElementById('<%= btn_lock.ClientID %>').style.display = "none";
                     currentState = 'hide';
                 }
                 else {
                     document.getElementById('<%= btn_lock.ClientID %>').style.display = "block";
                     currentState = 'show';
                 }
             }
</script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%">
                <center class="style56">
                    DEDUCTION ENTRY&nbsp; FORM</center>
            </td>
        </tr>
    </table>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
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
            <br />
            <asp:Panel ID="Panel1" runat="server">
                <div style="width: 100%;">
                    <table width="100%">
                        <tr>
                            <td align="right" width="50%">
                                &nbsp;
                            </td>
                            <td class="style3">
                                <div align="right">
                                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="fontt" NavigateUrl="~/RptDeduction_detailmaster.aspx">Deduction Report</asp:HyperLink></div>
                            </td>
                        </tr>
                        <table align="center" class="text2">
                            <tr align="center" class="text2">
                                <td colspan="2">
                                    <center>
                                        <fieldset width="100%">
                                            <legend style="font-family: Andalus; font-size: small; color: #CC0066">DEDUCTION ENTRY</legend>
                                            <table id="table12" border="0" cellspacing="1" width="100%">
                                                <tr>
                                                    <td class="style1" colspan="3">
                                                        <table class="style2">
                                                            <tr align="center">
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <span class="style5">From Date</span><asp:TextBox ID="txt_FromDate" runat="server"
                                                                        CssClass="ddl2" Enabled="False" Font-Size="Small"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                        PopupButtonID="txt_FromDate" PopupPosition="TopRight" TargetControlID="txt_FromDate">
                                                                    </asp:CalendarExtender>
                                                                    <span class="style4"><span class="style7">To Date</span><asp:TextBox ID="txt_ToDate"
                                                                        runat="server" CssClass="ddl2" Enabled="False" Font-Size="Small"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="txt_ToDate_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                            PopupButtonID="txt_ToDate" PopupPosition="TopRight" TargetControlID="txt_ToDate">
                                                                        </asp:CalendarExtender>
                                                                    </span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1" width="10%">
                                                        <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" Height="16px"
                                                            Visible="false" Width="29px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="right" width="20%">
                                                        <asp:Label ID="Label2" runat="server" CssClass="style6" Text="Plant Name"></asp:Label>
                                                    </td>
                                                    <td align="left" width="40%">
                                                        <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" CssClass="ddl2"
                                                            Font-Bold="True" Font-Size="12px" OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged"
                                                            Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="10%">
                                                        <asp:Label ID="lbl_RouteID" runat="server" Text="Route ID" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="right" width="20%">
                                                        <asp:Label ID="lbl_RouteName" runat="server" Text="Route Name" CssClass="style6"></asp:Label>
                                                    </td>
                                                    <td align="left" width="40%">
                                                        <asp:DropDownList ID="ddl_RouteName" runat="server" AutoPostBack="True" CssClass="ddl2"
                                                            Font-Bold="True" Font-Size="12px" OnSelectedIndexChanged="ddl_RouteName_SelectedIndexChanged"
                                                            Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1" width="10%">
                                                        <asp:TextBox ID="txt_AgentId" runat="server" Height="16px" TabIndex="2" Visible="False"
                                                            Width="40px"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="20%">
                                                        <asp:Label ID="lbl_AgentId" runat="server" Text="Agent Id" CssClass="style6"></asp:Label>
                                                    </td>
                                                    <td align="left" width="40%">
                                                        <asp:DropDownList ID="ddl_AgentName" runat="server" AutoPostBack="True" CssClass="ddl2"
                                                            Font-Bold="True" Font-Size="12px" OnSelectedIndexChanged="ddl_AgentName_SelectedIndexChanged"
                                                            TabIndex="2" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="10%" class="style1">
                                                    </td>
                                                    <td align="right" width="20%" class="style1">
                                                        <asp:Label ID="Lbl_selectedReportItem" runat="server" Visible="false"></asp:Label>
                                                    </td>
                                                    <td align="left" class="style1" width="40%">
                                                        <asp:RadioButtonList ID="rbtLstReportItems" RepeatDirection="Horizontal" RepeatLayout="Table"
                                                            AutoPostBack="true" runat="server" OnSelectedIndexChanged="rbtLstReportItems_SelectedIndexChanged">
                                                            <asp:ListItem Text="feed" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Billadv" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Material" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Can" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="Dpu" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="Others" Value="6"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" width="10%" class="style1">
                                                        <asp:Label ID="lbl_AgentName" runat="server" Text="Agent Name" Visible="False"></asp:Label>
                                                    </td>
                                                    <td align="right" width="20%" class="style1">
                                                        <asp:Label ID="lbl_Billadvance" runat="server" Text="Can" CssClass="style6"></asp:Label>
                                                    </td>
                                                    <td align="left" class="style1" width="40%">
                                                        <asp:TextBox ID="txt_Can" runat="server" CssClass="ddl2" Font-Bold="True" Font-Size="Small"
                                                            TabIndex="3"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_Can"
                                                            CssClass="money" Display="dynamic" ErrorMessage="*" ValidationExpression="[0-9,.]{1,10}"> </asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1" width="10%">
                                                        <asp:TextBox ID="txt_AgentName" runat="server" OnTextChanged="txt_AgentName_TextChanged"
                                                            TabIndex="3" Visible="False" Width="42px"></asp:TextBox>
                                                    </td>
                                                    <td align="right" width="20%">
                                                        <asp:Label ID="lbl_Medicines" runat="server" Text="Medicines" CssClass="style6"></asp:Label>
                                                    </td>
                                                    <td align="left" class="style1" width="40%">
                                                        <asp:TextBox ID="txt_Medicines" runat="server" CssClass="ddl2" TabIndex="4" Font-Size="Small"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_Medicines"
                                                            Display="dynamic" ErrorMessage="*" ValidationExpression="[0-9,.]{1,10}" CssClass="money"> </asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1" width="10%">
                                                        <asp:DropDownList ID="ddl_RouteID" runat="server" AutoPostBack="True" Font-Bold="False"
                                                            OnSelectedIndexChanged="ddl_RouteID_SelectedIndexChanged" Visible="false" Width="30px">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddl_AgentId" runat="server" AutoPostBack="True" Height="16px"
                                                            OnSelectedIndexChanged="ddl_AgentId_SelectedIndexChanged" Visible="False" Width="35px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="right" width="20%">
                                                        <asp:Label ID="lbl_Feed" runat="server" Text="Feed" CssClass="style6"></asp:Label>
                                                    </td>
                                                    <td align="left" width="40%">
                                                        <asp:TextBox ID="txt_Feed" runat="server" CssClass="ddl2" TabIndex="5" Font-Size="Small"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_Feed"
                                                            Display="dynamic" ErrorMessage="*" ValidationExpression="[0-9,.]{1,10}" CssClass="money"> </asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1" width="10%">
                                                    </td>
                                                    <td align="right" width="20%">
                                                        <asp:Label ID="lbl_Can" runat="server" Text=" Bill Advance" CssClass="style6"></asp:Label>
                                                    </td>
                                                    <td align="left" width="40%">
                                                        <asp:TextBox ID="txt_Billadvance" runat="server" CssClass="ddl2" TabIndex="6" Font-Size="Small"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="ValidateName2" runat="server" ControlToValidate="txt_Billadvance"
                                                            CssClass="money" Display="dynamic" ErrorMessage="*" ValidationExpression="[0-9,.]{1,10}"> </asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1" width="10%">
                                                    </td>
                                                    <td align="right" width="20%">
                                                        <asp:Label ID="lbl_Recovery" runat="server" Text="Recovery" CssClass="style6"></asp:Label>
                                                    </td>
                                                    <td align="left" width="40%">
                                                        <asp:TextBox ID="txt_Recovery" runat="server" CssClass="ddl2" TabIndex="7" Font-Size="Small"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txt_Recovery"
                                                            Display="dynamic" ErrorMessage="*" ValidationExpression="[0-9,.]{1,10}" CssClass="money"> </asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="10%">
                                                        &nbsp;
                                                    </td>
                                                    <td align="right" width="20%">
                                                        <asp:Label ID="lbl_Others" runat="server" Text="Others" CssClass="style6"></asp:Label>
                                                    </td>
                                                    <td align="left" width="40%">
                                                        <asp:TextBox ID="txt_Others" runat="server" CssClass="ddl2" TabIndex="8" Font-Size="Small"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_Others"
                                                            Display="dynamic" ErrorMessage="*" ValidationExpression="[0-9,.]{1,10}" CssClass="money"> </asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table id="table2">
                                                <tr>
                                                    <td style="width: 30%">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left" style="width: 30%">
                                                        <left>
                                                      <asp:Button ID="btn_Save" runat="server" BackColor="#6F696F" 
                                                          CssClass="buttonclass" ForeColor="White" onclick="btn_Save_Click" TabIndex="10" 
                                                          Text="Save" Width="58px" />
                                                      
                                                      <br />
                                                      <asp:Label ID="lbl_msg" runat="server" 
                                                          style="font-family: Andalus; font-size: medium" Text="Label"></asp:Label>
                                                  </left>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <div>
                                                            <asp:Button ID="btn_modsample" runat="server" Style="display: none" />
                                                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                                                PopupControlID="pnlpopup" TargetControlID="btn_modsample">
                                                            </asp:ModalPopupExtender>
                                                            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Style="display: inline">
                                                                <fieldset class="fontt">
                                                                    <div align="right">
                                                                        <asp:Button ID="btn_Mclose" runat="server" BackColor="#6F696F" Font-Bold="true" ForeColor="White"
                                                                            OnClick="btn_Mclose_Click" Text="X" />
                                                                    </div>
                                                                    <legend style="color: #3399FF">Deduction Allotment </legend>
                                                                    <table id="table4" align="center" border="0" cellspacing="1" width="100%">
                                                                        <tr>
                                                                            <td width="40%">
                                                                            </td>
                                                                            <td width="40%">
                                                                            </td>
                                                                            <td width="10%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="40%" style="text-align: right">
                                                                                <asp:Label ID="lbl_CategoryType" runat="server" Text="CategoryType"></asp:Label>
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:DropDownList ID="ddl_CategoryType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_CategoryType_SelectedIndexChanged"
                                                                                    Width="149px">
                                                                                    <asp:ListItem>Feed50kg</asp:ListItem>
                                                                                    <asp:ListItem>Feed100kg</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td width="10%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="40%" style="text-align: right">
                                                                                <asp:Label ID="lbl_CategoryAvail" runat="server" Text="Avail Balance" Style="text-align: right"></asp:Label>
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:TextBox ID="txt_CategoryAvail" runat="server" Enabled="False" Width="149px" />
                                                                            </td>
                                                                            <td width="10%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="40%" style="text-align: right">
                                                                                <asp:Label ID="lbl_rate" runat="server" Text="Rate"></asp:Label>
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:TextBox ID="txt_rate" runat="server" Enabled="False" Width="149px" />
                                                                            </td>
                                                                            <td width="10%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="40%" style="text-align: right">
                                                                                <asp:Label ID="lbl_Quantity" runat="server" Text="Quantity"></asp:Label>
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:TextBox ID="txt_Quantity" runat="server" AutoPostBack="true" OnTextChanged="txt_Quantity_TextChanged"
                                                                                    Width="149px" />
                                                                            </td>
                                                                            <td width="10%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="40%" style="text-align: right">
                                                                                <asp:Label ID="lbl_Amount" runat="server" Text="Amount"></asp:Label>
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:TextBox ID="txt_Amount" runat="server" Enabled="false" Width="149px" />
                                                                            </td>
                                                                            <td width="10%">
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td colspan="3" style="width: 50%">
                                                                                <asp:Button ID="btnCreateratechart" runat="server" CssClass="buttonclass" Font-Bold="true"
                                                                                    OnClick="btnCreateratechart_Click" Style="font-weight: 700; font-family: Andalus;
                                                                                    font-size: small;" TabIndex="14" Text="Create" Width="55px" />
                                                                                <asp:Button ID="btn_clear" runat="server" BorderStyle="Groove" BorderWidth="1px"
                                                                                    CssClass="buttonclass" OnClick="btn_clear_Click" OnClientClick="return confirm('Are you sure you want to Clear this record?');"
                                                                                    Style="font-weight: 700; font-family: Andalus; font-size: small;" TabIndex="15"
                                                                                    Text="Clear" Width="50px" />
                                                                                <asp:Button ID="btn_ModularSave" runat="server" BorderStyle="Groove" BorderWidth="1px"
                                                                                    CssClass="buttonclass" OnClick="btn_ModularSave_Click" OnClientClick="return confirm('Are you sure you want to Save this record?');"
                                                                                    Style="font-weight: 700; font-family: Andalus; font-size: small;" TabIndex="15"
                                                                                    Text="Save" Width="50px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" colspan="3">
                                                                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" Font-Size="8px" HeaderStyle-BackColor="White"
                                                                                    HeaderStyle-ForeColor="Brown" Height="79px" PageSize="5" Style="text-align: center;
                                                                                    font-size: small; font-family: Andalus;">
                                                                                    <Columns>
                                                                                        <asp:TemplateField>
                                                                                            <HeaderTemplate>
                                                                                                Sno
                                                                                            </HeaderTemplate>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblSRNO" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                            </asp:Panel>
                                                            <asp:Label ID="Lbl_Errormsg" runat="server" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </center>
                                    <%--</div>--%>&nbsp;<table class="style2">
                                        <tr class="text2">
                                            <td valign="top">
                                                <center>
                                                    <asp:GridView ID="gvProducts" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        CssClass="table table-striped table-bordered table-hover" DataKeyNames="tid"
                                                        Font-Size="Medium" OnPageIndexChanging="gvProducts_PageIndexChanging" OnRowCancelingEdit="gvProducts_RowCancelingEdit"
                                                        OnRowEditing="gvProducts_RowEditing" OnRowUpdating="gvProducts_RowUpdating" EnableViewState="False"
                                                        OnRowDeleting="gvProducts_RowDeleting">
                                                        <HeaderStyle ForeColor="#660066" />
                                                        <Columns>
                                                            <asp:BoundField DataField="tid" HeaderText="Tid" ReadOnly="True" SortExpression="tid" />
                                                            <asp:BoundField DataField="Agent_id" HeaderText="Agent ID" ReadOnly="True" SortExpression="Agent_id" />
                                                            <asp:BoundField DataField="route_id" HeaderText="Route Id" ReadOnly="True" SortExpression="route_id" />
                                                            <asp:BoundField DataField="billadvance" HeaderText="Advance" SortExpression="billadvance">
                                                                <ControlStyle Width="60px" />
                                                                <FooterStyle Width="60px" />
                                                                <HeaderStyle Width="60px" />
                                                                <ItemStyle Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ai" HeaderText="AI" SortExpression="ai">
                                                                <ControlStyle Width="60px" />
                                                                <FooterStyle Width="60px" />
                                                                <HeaderStyle Width="60px" />
                                                                <ItemStyle Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="feed" HeaderText="Feed" SortExpression="feed">
                                                                <ControlStyle Width="60px" />
                                                                <FooterStyle Width="60px" />
                                                                <HeaderStyle Width="60px" />
                                                                <ItemStyle Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="can" HeaderText="Can" SortExpression="can">
                                                                <ControlStyle Width="60px" />
                                                                <FooterStyle Width="60px" />
                                                                <HeaderStyle Width="60px" />
                                                                <ItemStyle Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="recovery" HeaderText="Recovery" SortExpression="recovery">
                                                                <ControlStyle Width="60px" />
                                                                <FooterStyle Width="60px" />
                                                                <HeaderStyle Width="60px" />
                                                                <ItemStyle Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="others" HeaderText="Others" SortExpression="others">
                                                                <ControlStyle Width="60px" />
                                                                <FooterStyle Width="60px" />
                                                                <HeaderStyle Width="60px" />
                                                                <ItemStyle Width="60px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="deductiondate" HeaderText="DeducDate" ReadOnly="True"
                                                                ShowHeader="False" SortExpression="deductiondate" />
                                                            <asp:CommandField ShowDeleteButton="True" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <br />
                                                    <strong>
                                                        <asp:Button ID="btn_lock" runat="server" BorderStyle="Groove" BorderWidth="1px" CausesValidation="False"
                                                            CssClass="btn" Font-Bold="True" Height="30px" OnClick="btn_lock_Click" Style="font-weight: 700;
                                                            font-family: Andalus; font-size: small;" Text="Approval" Width="89px" />
                                                    </strong>
                                                </center>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                </div>
            </asp:Panel>
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_Save" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
