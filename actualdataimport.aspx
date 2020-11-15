<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="actualdataimport.aspx.cs" Inherits="actualdataimport" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 20%;
        }
        .style2
        {
            width: 2%;
        }
        .style6
        {
            font-family: Verdana,Arial,Helvetica,sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            text-decoration: none;
            word-spacing: normal;
            letter-spacing: normal;
            text-transform: none;
            text-decoration: none;
            BACKGROUND: none;
            color: black;
            width: 30%;
        }
        .style4
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            color: #3399FF;
            width: 9%;
        }
        
        .modalPopup
{
background-color: #696969;
filter: alpha(opacity=80);
opacity: 0.15;
xindex:-1;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <div  >
<asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>       
        <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup"  />
</div>  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;IMPORT PROCUREMENT</p>
            </td>
        </tr>
        <tr>
            <td width="100%" height="3px">
            </td>
        </tr>
        <tr>
            <td width="100%" class="line" height="1px">
            </td>
        </tr>
        <tr>
            <td width="100%" height="7">
                
                <div style="width:100%;">
                    <table width="100%" style="margin-top: 14px">
                    <tr>
                            <td class="style1">
                                <br />
                            </td>
                            <td class="fontt" width="10%">
                                
                            <td align="right" class="fontt" width="15%">
                                <asp:CheckBox ID="Chk_periodsess" runat="server" AutoPostBack="true" 
                                    Text="Period" oncheckedchanged="Chk_periodsess_CheckedChanged" 
                                    Checked="True"  /></td></td>
                            <td class="style2">
                                &nbsp;</td>
                            <td class="fontt" width="9%">
                                <asp:Label ID="lbl_sess" runat="server" Text="Sessions"></asp:Label></td>
                            <td align="left" class="fontt" width="15%">
                                <asp:DropDownList ID="ddl_ses" 
        runat="server" Width="60px" 
         AutoPostBack="True" 
         Font-Bold="True" Font-Size="Medium" >
     <asp:ListItem>AM</asp:ListItem>
     <asp:ListItem>PM</asp:ListItem>     
    </asp:DropDownList>                           
                            </td>
                            <td width="12%">
                                </td>
                            <td width="20%">
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <br />
                            </td>
                            <td class="fontt" width="10%">
                                From Date:</td>
                            <td align="right" class="fontt" width="15%">
                                <asp:TextBox ID="txt_FromDate" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" 
                                    PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                    TargetControlID="txt_FromDate">
                                </asp:CalendarExtender>
                            </td>
                            <td class="style2">
                                &nbsp;</td>
                            <td class="fontt" width="9%">
                                To Date:</td>
                            <td align="right" class="fontt" width="15%">
                                <asp:TextBox ID="txt_ToDate" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy" 
                                    PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                                    TargetControlID="txt_ToDate">
                                </asp:CalendarExtender>
                            </td>
                            <td width="12%">
                                &nbsp;</td>
                            <td width="20%">
                            </td>
                        </tr>
                    </table>
                </div>
                <table border="0" cellpadding="0" cellspacing="1" 
                    style="margin-top: 22px; margin-bottom: 13px" width="100%">
                    <tr>
                        <td class="style6">
                            <br />
                        </td>
                        <td class="style4">
                            <table ID="table12" border="0" cellspacing="1">
                                <tr>
                                    <td align="left" width="25%">
                                        Plantname
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="fontt" width="20%">
                            <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true"  Font-Bold="True" Font-Size="Medium"
                                OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged" Width="154px">
                            </asp:DropDownList>
                        </td>
                        <td class="td1">
                            <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                OnSelectedIndexChanged="ddl_Plantcode_SelectedIndexChanged" Visible="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="1" 
                    style="height: 30px; margin-bottom: 14px; margin-left: 0px;" width="100%">
                    <tr>
                        <td class="td1">
                        </td>
                        <td class="style1">
                            <asp:Button ID="btn_show" runat="server" BackColor="#6F696F" Font-Bold="False" 
                                ForeColor="White" onclick="btn_delete_Click" style="margin-left: 31px" 
                                TabIndex="1" Text="OK" Width="86px" OnClientClick="return confirm('Do U want to Transfer the records?');" />
                            <asp:Button ID="btn_sess" runat="server" BackColor="#6F696F" Font-Bold="False" 
                                ForeColor="White" onclick="btn_sess_Click" 
                                OnClientClick="return confirm('Do U want to Transfer the records?');" 
                                style="margin-left: 31px" TabIndex="1" Text="OKses" Width="86px" />
                        </td>
                       
                        <td class="td1">
                        </td>
                    </tr>
                    <tr align=center>
                        <td class="td1" colspan="3">
                            <asp:GridView ID="GridView2" runat="server" CssClass="gridcls" Font-Bold="True" Font-Size="12px" ForeColor="White" GridLines="Both" Width="30%">
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                <HeaderStyle BackColor="#f4f4f4" Font-Bold="False" Font-Italic="False" 
                                    Font-Names="Raavi" Font-Size="Small" ForeColor="Black" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                <AlternatingRowStyle HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <Columns>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        </table>
        <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
 </ContentTemplate>
 </asp:UpdatePanel>
  
</asp:Content>


