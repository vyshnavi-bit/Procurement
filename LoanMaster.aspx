<%@ Page Title="OnlineMilkTest|Loanmaster" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="LoanMaster.aspx.cs" Inherits="LoanMaster" %>

<%--<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            height: 30px;
            text-align: left;
        }
        
        .modalPopup
        {
            background-color: #696969;
            filter: alpha(opacity=80);
            opacity: 0.15;
            xindex: -1;
        }
        #table1
        {
            height: 62px;
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        //
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }       

    </script>
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
        
        
        
        .style2
        {
            width: 100%;
        }
        
        
        
        .style3
        {
            height: 30px;
        }
    </style>
    <%-- <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #FFFFFF; font-size: 36px; left: 40%; top: 40%;">Loading ...</span>
        </div>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server" />
    <div>
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <%-- <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
            <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #FFFFFF; font-size: 36px; left: 40%; top: 40%;">Loading ...</span>
        </div>--%>
                <div style="position: fixed; text-align: center; height: 10%; width: 1100px; top: 0;
                    right: 0; left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
                </div>
            </ProgressTemplate>
            <%--<ProgressTemplate>
<asp:Image ID="Image1" ImageUrl="waiting.gif" AlternateText="Processing" runat="server" />
</ProgressTemplate>--%>
        </asp:UpdateProgress>
        <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
            PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td width="100%">
                            <p class="subheading" style="line-height: 150%">
                                &nbsp;&nbsp;LOAN MASTER
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" class="line" height="1px">
                        </td>
                    </tr>
                </table>
                <center>
                <table align="center" class="text2">
                <tr align="center">
                <td>
                <div  ALIGN="center" style="width: 60%; margin-left: 0px;" >
                    <table ALIGN="center" width="60%">
                        <tr>
                            <td class="style3" style="text-align: center">
                                From</td>
                            <td class="style3" style="text-align: center">
                                <asp:TextBox ID="txt_LoanDate" runat="server" Enabled="False"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                    PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                    TargetControlID="txt_LoanDate">
                                </asp:CalendarExtender>
                            </td>
                            <td class="style3" style="text-align: center">
                                To
                            </td>
                            <td class="style3" style="text-align: center">
                                <asp:TextBox ID="txt_LoanExpireDate" runat="server" Enabled="False"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                                    PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                                    TargetControlID="txt_LoanExpireDate">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr WIDTH="70%">
                            <td align="center" WIDTH="70%" colspan="4">
                                <div  align="center"   WIDTH="60%">
                                    <fieldset  width=50%>
                                        <legend  >LOAN MASTER</legend>
                                        <table ID="table12" border="0" cellspacing="1" width="70%">
                                            <tr>
                                                <td class="style1" width="10%">
                                                    <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                                        Height="16px" onselectedindexchanged="ddl_Plantcode_SelectedIndexChanged" 
                                                        Visible="false" Width="29px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style1" width="20%">
                                                    <asp:Label ID="Label3" runat="server" Text="Plant Name"></asp:Label>
                                                </td>
                                                <td class="style1" style="width:40%;">
                                                    <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
                                                        Font-Bold="True" Font-Size="Large" 
                                                        OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged" Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" width="10%">
                                                    <asp:Label ID="lbl_AgentId" runat="server" Text="Agent Id" Visible="False"></asp:Label>
                                                </td>
                                                <td class="style1" width="20%">
                                                    <asp:Label ID="lbl_RouteName" runat="server" Text="Route Name"></asp:Label>
                                                </td>
                                                <td class="style1" width="40%">
                                                    <asp:DropDownList ID="ddl_RouteName" runat="server" AutoPostBack="True" 
                                                        Font-Bold="True" Font-Size="Large" 
                                                        OnSelectedIndexChanged="ddl_RouteName_SelectedIndexChanged" TabIndex="1" 
                                                        Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" width="10%">
                                                    &nbsp;</td>
                                                <td class="style1" width="20%">
                                                    <asp:Label ID="lbl_AgentName" runat="server" Text="Agent Name"></asp:Label>
                                                </td>
                                                <td class="style1" width="40%">
                                                    <asp:DropDownList ID="ddl_AgentName" runat="server" Font-Bold="True" 
                                                        Font-Size="Large" Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" width="10%">
                                                    <asp:Label ID="lbl_RouteID" runat="server" Text="Route ID" Visible="false"></asp:Label>
                                                    <asp:DropDownList ID="ddl_RouteID" runat="server" AutoPostBack="True" 
                                                        Height="18px" OnSelectedIndexChanged="ddl_RouteID_SelectedIndexChanged" 
                                                        Visible="false" Width="20px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style1" width="20%">
                                                    <asp:Label ID="lbl_loanId" runat="server" Text="loan Id"></asp:Label>
                                                </td>
                                                <td class="style1" width="40%">
                                                    <asp:TextBox ID="txt_loanId" runat="server" Enabled="False" TabIndex="4"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" width="10%">
                                                    <asp:TextBox ID="txt_AgentId" runat="server" TabIndex="2" Visible="False" 
                                                        Width="39px"></asp:TextBox>
                                                </td>
                                                <td class="style1" width="20%">
                                                    <asp:Label ID="lbl_LoanAmount" runat="server" Text="Loan Amount"></asp:Label>
                                                </td>
                                                <td class="style1" width="40%">
                                                    <asp:TextBox ID="txt_LoanAmount" runat="server" TabIndex="5"></asp:TextBox>
                                                    Rs
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                        ControlToValidate="txt_LoanAmount" ErrorMessage="*" 
                                                        validationExpression="^[0-9,.]{1,10}$"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" width="10%">
                                                    <asp:TextBox ID="txt_AgentName" runat="server" Enabled="False" Height="22px" 
                                                        TabIndex="3" Visible="False" Width="32px"></asp:TextBox>
                                                </td>
                                                <td class="style1" width="20%">
                                                    <asp:Label ID="Label1" runat="server" Text="Interest_Amount"></asp:Label>
                                                </td>
                                                <td class="style1" width="40%">
                                                    <asp:TextBox ID="txt_InterestRate" runat="server" Height="22px" TabIndex="6"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                                        ControlToValidate="txt_InterestRate" ErrorMessage="*" 
                                                        validationExpression="^[0-9,.]{1,10}$"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" width="10%">
                                                </td>
                                                <td class="style1" width="20%">
                                                    <asp:Label ID="lbl_NoofInstalment" runat="server" Text="No of Instalment"></asp:Label>
                                                </td>
                                                <td class="style1" width="40%">
                                                    <asp:TextBox ID="txt_NoofInstalment" runat="server" AutoPostBack="True" 
                                                        OnTextChanged="txt_NoofInstalment_TextChanged" TabIndex="7"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                        ControlToValidate="txt_NoofInstalment" ErrorMessage="*" 
                                                        validationExpression="^[0-9,.]{1,10}$"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" width="10%">
                                                </td>
                                                <td class="style1" width="20%">
                                                    <asp:Label ID="lbl_InstalAmount" runat="server" Text="InstalAmount"></asp:Label>
                                                </td>
                                                <td class="style1" width="40%">
                                                    <asp:TextBox ID="txt_InstalAmount" runat="server" Enabled="False" TabIndex="8"></asp:TextBox>
                                                    Rs
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                        ControlToValidate="txt_InstalAmount" ErrorMessage="*" 
                                                        validationExpression="^[0-9,.]{1,10}$"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%">
                                                </td>
                                                <td width="20%" style="text-align: left">
                                                    <asp:Label ID="lbl_loanDescription" runat="server" Text="loan Description"></asp:Label>
                                                </td>
                                                <td width="40%" style="text-align: left">
                                                    <asp:TextBox ID="txt_loanDescription" runat="server" Height="36px" TabIndex="9" 
                                                        TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                        ControlToValidate="txt_loanDescription" ErrorMessage="*" 
                                                        ValidationExpression="^[a-zA-Z,.]"> </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td colspan="3" style="width: 50%">
                                                    <asp:Button ID="btn_Save" runat="server" BackColor="#6F696F" ForeColor="White" 
                                                        OnClick="btn_Save_Click" OnClientClick="" TabIndex="10" Text="Save" 
                                                        Width="56px" />
                                                    <asp:Button ID="btn_Export" runat="server" BackColor="#6F696F" 
                                                        ForeColor="White" OnClick="btn_Export_Click" OnClientClick="" TabIndex="10" 
                                                        Text="Export" Width="56px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                </td>
                </tr>
                </table>
                </center>
                <br />
                <table class="style2">
                    <tr align="center">
                        <td>
                            <asp:Label ID="Lbl_Errormsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center" class="text2">
                        <td align="center">
                            <table align="center" class="text2">
                                <tr align="center">
                                    <td valign="top">
                                        <asp:GridView ID="gvProducts" runat="server" CssClass="table table-striped table-bordered table-hover"
                                            Font-Size="Medium" OnPageIndexChanging="gvProducts_PageIndexChanging" OnRowCancelingEdit="gvProducts_RowCancelingEdit"
                                            PageSize="20">
                                            <HeaderStyle ForeColor="#660066" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNO" ItemStyle-Width="50">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <table class="style2">
                    <tr align="center">
                        <td>
                            <strong>
                                <asp:Button ID="btn_lock" runat="server" BorderStyle="Groove" BorderWidth="1px" CausesValidation="False"
                                    CssClass="btn" Font-Bold="True" Height="30px" OnClick="btn_lock_Click" Style="font-weight: 700;
                                    font-family: Andalus; font-size: small;" Text="Approval" Width="89px" />
                            </strong>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <div align="center" class="foo">
        <a href="home.aspx" class="fotmenu">Home</a><em style="color: White;">|</em> <a href="Agent.aspx"
            class="fotmenu">Agent</a> <em style="color: White;">|</em> <a onclick="window.open ('WeightSingle.aspx', '_blank', 'width=1280, height=1000, scrollbars=yes, resizable=yes, location=no, status=no, menubar=no, toolbar=no');"
                class="fotmenu">Weigher</a><em style="color: White;">|</em> <a onclick="window.open ('AnalizerSingle.aspx', '_blank', 'width=1280, height=1000, scrollbars=yes, resizable=yes, location=no, status=no, menubar=no, toolbar=no');"
                    class="fotmenu">Milk Analizer</a> <em style="color: White;">|</em> <a href="RateChart.aspx"
                        class="fotmenu">Rate chart</a> <em style="color: White;">|</em>
        <a href="Contact us.aspx" class="fotmenu">Contact</a>
    </div>
    <div align="center" style="font-family: 'Times New Roman', Times, serif; font-size: 13px;
        font-weight: inherit; text-transform: inherit; color: #FFFFFF; font-variant: normal;">
        <p class="fontt">
            Best viewed in and above | Best view 1024 x 768 screen resolution</p>
        <br />
        <!-- <a href="http://10solution.com" title="10solution In." target="_blank" style="background:White;   text-decoration: none;">
                      
                       <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/logo.png" /></a>-->
        -->
    </div>
</asp:Content>
