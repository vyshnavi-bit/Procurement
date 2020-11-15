<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Agent.aspx.cs" Inherits="Agent" Title="OnlineMilkTest|Agent Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />
<style type="text/css">
    .style1
    {
        height: 17px;
        width: 50%;
        text-align: left;
    }
    .style2
    {
        height: 32px;
    }
    .style21
    {
         width: 40%;
    }
    .style3
    {
        color: #3399FF;
    }
    .modalBackground
{
background-color: White;
}
    #table3
    {
        height: 29px;
        width: 100%;
    }
    .style22
    {
        height: 27px;
    }
    .style23
    {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-weight: normal;
        font-style: normal;
        color: #3399FF;
        font-size: 9pt;
        text-align: center;
        height: 27px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%" colspan="2">
                <br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;AGENT MASTER
                </p>
            </td>
        </tr>
        <tr>
            <td width="100%" height="3px" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="100%" class="line" height="1px" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="100%" height="7" colspan="2">
            </td>
        </tr>
    </table>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table align="center" border="0" width="100%" id="Datetable" cellspacing="1">
        <tr>
            <td width="35%" class="style22">
            </td>
            <td width="28%" class="style23" align="center">
                </td>
            <td width="20%" class="style23" align="right">
                <span class="style3">Date</span>
            </td>
            <td width="12%" class="style22">
                <asp:TextBox ID="txt_fromdate" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td width="5%" class="style22">
                <asp:ImageButton ID="popupcal" runat="server" ImageUrl="~/calendar.gif" Height="20px" />
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_fromdate"
                    PopupButtonID="popupcal" Format="dd/MM/yyyy" PopupPosition="TopRight">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr align="center">
            <td  align="center" colspan="5">
                <asp:Panel ID="Panel1"  align="center" runat="server" Width=30%>
                <div align="center">
  <asp:Button ID="btn_modsample" runat="server" Visible="False" />
    <%--<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btn_modsample" PopupControlID="pnlpopup" 
 BackgroundCssClass="modalBackground"> </asp:ModalPopupExtender>--%>
 <asp:Panel ID="pnlpopup" runat="server" BackColor="White"  style="display:inline">

 <center>
        <fieldset>
        <div align="right">
                <asp:HyperLink ID="home" runat="server" NavigateUrl="~/home.aspx" 
                    Font-Bold="True" style="text-align: right" >Close</asp:HyperLink>
                </div>
            <legend style="color: #3399FF">MODIFY AGENT </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">
            <tr>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Height="16px" Width="61px" Visible="false"></asp:TextBox>
            </td>
            </tr>
            <tr>
                    <td>
                    </td>
                    <td  align="right">
                        Plant Name
                    </td>
                    <td  align="left">
                        <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" TabIndex="2" 
                            Width="149px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td style="text-align: right" >
                        Route Name </td>
                    <td style="text-align: left" >
                        <asp:DropDownList ID="cmb_RouteName" runat="server" AutoPostBack="True" 
                            OnSelectedIndexChanged="cmb_RouteName_SelectedIndexChanged" TabIndex="2" 
                            Width="149px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td style="text-align: right">
                        <asp:RadioButton ID="rd_mcom" runat="server" AutoPostBack="True" Checked="True" 
                            CssClass="style3" Font-Bold="True" ForeColor="#3399FF" 
                            oncheckedchanged="rd_mcom_CheckedChanged" Text="Cow" />
                    </td>
                    <td style="text-align: left">
                        <asp:RadioButton ID="rd_mbuff" runat="server" AutoPostBack="True" 
                            CssClass="style3" Font-Bold="True" ForeColor="#3399FF" 
                            oncheckedchanged="rd_mbuff_CheckedChanged" Text="Buffalo" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddl_mAgentid" runat="server"  
                             Width="61px" Visible=false Height="16px">
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        Agent ID
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_mAgentname" runat="server" Width="149px"                            
                            AutoPostBack="True" 
                            onselectedindexchanged="ddl_mAgentname_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td >
                        <asp:DropDownList ID="ddl_mAgentmobno" runat="server" Height="16px" 
                            Visible="false" Width="61px">
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        Agent Name
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_magentname" runat="server" TabIndex="4" Width="149px" />
                    </td>
                </tr>
                <tr>
                    <td >
                    </td>
                    <td align="left">
                        Agent Mobile</td>
                    <td align="left">
                        <asp:TextBox ID="txt_magentmobno" runat="server" Width="142px" TabIndex="5"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ControlToValidate="txt_magentmobno" ErrorMessage="*" 
                            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td >
                    </td>
                    <td align="left">
                        Agent Type
                    </td>
                    <td align="left">
                        &nbsp;<asp:RadioButton ID="rd_mbulk" runat="server" AutoPostBack="True" 
                            Checked="True" CssClass="style3" Font-Bold="True" ForeColor="#3399FF" 
                             Text="Bulk" oncheckedchanged="rd_mbulk_CheckedChanged" />
                        <asp:RadioButton ID="rd_mcentre" runat="server" AutoPostBack="True" 
                            CssClass="style3" Font-Bold="True" ForeColor="#3399FF" 
                             Text="Centre" oncheckedchanged="rd_mcentre_CheckedChanged" 
                            Visible="False" />

                    </td>
                </tr>
                <tr>
                    <td >
                    </td>
                    <td align="left">
                        Cartage
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_mcartage" runat="server" TabIndex="7" Width="80px" />
                        <span class="style3">Rs.Paise
                    </span>
                    </td>
                </tr>
                 <tr>
                    <td >
                    </td>
                    <td align="left">
                        SplBonus
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_msplbonus" runat="server" TabIndex="8" Width="80px" />
                        <span class="style3">Rs.Paise
                    </span>
                    </td>
                </tr>
                <tr>
                    <td >
                    </td>
                    <td >
                       
                        &nbsp;</td>
                    <td>
                        <asp:CheckBox ID="chk_maccountstatus" runat="server" Text="Account" 
                            AutoPostBack="True" 
                            style="color: #3399FF" 
                            oncheckedchanged="chk_maccountstatus_CheckedChanged" />
                    </td>
                </tr>
                 <tr>
                    <td >
                        <asp:DropDownList ID="ddl_MBankId" runat="server" Height="16px" 
                           Visible="False" 
                            Width="57px" onselectedindexchanged="ddl_MBankId_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_mbank" runat="server" Text="Select Bank"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_mBankName" runat="server"  Width="149px" 
                            TabIndex="8" 
                            AutoPostBack="True" 
                            onselectedindexchanged="ddl_mBankName_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        &nbsp;</td>
                    <td align="left">
                        <asp:Label ID="lbl_mbranname" runat="server" Text="Select Branch"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_mbranchname" runat="server" AutoPostBack="True" 
                             Width="149px" 
                            onselectedindexchanged="ddl_mbranchname_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                
                <tr>
                    <td >
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_mifsccode" runat="server" Text="Ifs Code"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="ddl_mIfsc" runat="server"  Width="149px" 
                           TabIndex="9" AutoPostBack="True" onselectedindexchanged="ddl_mIfsc_SelectedIndexChanged" 
                           >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left">
                        <asp:Label ID="lbl_maccno" runat="server" Text="Agent AC-No"></asp:Label> 
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_mAgentAccNo" runat="server"  Width="149px" TabIndex="13" />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="txt_mAgentAccNo" validationExpression="^[0-9,.]{1,10}$" ErrorMessage="*"></asp:RequiredFieldValidator>

                    </td>
                </tr>
                
            </table>
            <table border="0" width="100%" id="table5" cellspacing="1" align="center">
                <tr>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td align="right">
                        <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Update" 
                            onclick="btnUpdate_Click" Visible="False"/> 
                             <asp:Button ID="btn_updatecancel" runat="server" Text="Reset" BackColor="#6F696F"
                            ForeColor="White" Width="60px" TabIndex="0" Visible="False" 
                            onclick="btn_updatecancel_Click" />
                        <asp:RadioButton ID="rd_update" runat="server" AutoPostBack="True" 
                            BackColor="#666699" BorderColor="#9900FF" BorderStyle="Ridge" BorderWidth="1px" 
                            oncheckedchanged="rd_update_CheckedChanged" Text="Update" Font-Bold="True" 
                            ForeColor="White" />
                    </td>
                </tr>
            </table>
        </fieldset>
      </center>
</asp:Panel>
 
  </div>
                </asp:Panel>
            </td>
        </tr>
        <tr align="center">
            <td colspan="5">

          
                <asp:Panel ID="details" runat="server" Visible="false">
                
             <fieldset class="style21">
            <legend style="color: #3399FF">ADD AGENT</legend>
            <table border="0" width="70%" id="table12" cellspacing="1">
            <tr>
                    <td width="20%" class="style2">
                        &nbsp;</td>
                    <td width="30%" class="style2">
                        <asp:RadioButton ID="rbcow" runat="server" AutoPostBack="True" Checked="True" 
                 Font-Bold="True" ForeColor="#3399FF" oncheckedchanged="rbcow_CheckedChanged" 
                 Text="Cow" CssClass="style3" />
                    </td>
                    <td width="20%" class="style2">
                        <asp:RadioButton ID="rbbuff" runat="server" AutoPostBack="True" 
                 Font-Bold="True" ForeColor="#3399FF" oncheckedchanged="rbbuff_CheckedChanged" 
                 Text="Buffalo" CssClass="style3" />
                    </td>
                </tr>
                <tr>
                    <td width="20%" colspan="2" style="width: 50%; text-align: right;">
                        <asp:Label ID="lbl_RouteID" runat="server" Text="Route ID" Visible="False" 
                            CssClass="style3"></asp:Label>
                    </td>
                    <td width="20%" align="right" style="text-align: left">
                <asp:CheckBox ID="chk_modifyagent" Text="Modify Agent" runat="server" 
                    AutoPostBack="false" oncheckedchanged="chk_modifyagent_CheckedChanged" 
                            Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td width="20%" colspan="2" style="width: 50%; text-align: right;">
                        &nbsp;</td>
                    <td width="20%" style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="20%" colspan="2" style="width: 50%; text-align: right;">
                        Route Name
                    </td>
                    <td width="20%" style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td width="20%" colspan="2" style="width: 50%; text-align: right;">
                        Agent ID
                    </td>
                    <td width="20%" style="text-align: left">
                        <asp:TextBox ID="txtAgentID" runat="server" TabIndex="3" 
                            Width="149px"  />
                    </td>
                </tr>
                <tr>
                    <td width="20%" colspan="2" style="width: 50%; text-align: right;">
                        Agent Name
                    </td>
                    <td width="20%" style="text-align: left">
                        <asp:TextBox ID="txtAgentName" runat="server" TabIndex="4" Width="149px" />
                    </td>
                </tr>
                <tr>
                    <td   colspan="2" style="text-align: right">
                        Agent Mobile</td>
                    <td width="20%" class="style1">
                        <asp:TextBox ID="txt_agentmobile" runat="server" Width="142px" TabIndex="5"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ValidationExpression="^[0-9,.]{1,10}$" ControlToValidate="txt_agentmobile"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td width="20%" colspan="2" style="width: 50%; text-align: right;">
                        Agent Type
                    </td>
                    <td width="20%" style="text-align: left">
                        <asp:DropDownList ID="Cmb_AgentType" runat="server" TabIndex="6" Width="60px">
                            <asp:ListItem Value="0">Bulk</asp:ListItem>
                            <asp:ListItem Value="1">Centre</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp; <span class="style3">Bulk/Centre </span>
                    </td>
                </tr>
                <tr>
                    <td width="20%" colspan="2" style="width: 50%; text-align: right;">
                        Cartage
                    </td>
                    <td width="20%" style="text-align: left">
                        <asp:TextBox ID="txt_CarAmt" runat="server" TabIndex="7" Width="80px" />
                        <span class="style3">Rs.Paise
                    </span>
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                    </td>
                    <td width="30%" style="text-align: right">
                       
                        <asp:DropDownList ID="ddl_BankId" runat="server"  Width="80px" 
                            onselectedindexchanged="ddl_BankId_SelectedIndexChanged" Visible="False">
                        </asp:DropDownList>
                       
                    </td>
                    <td width="20%" style="text-align: left">
                        <asp:CheckBox ID="Chk_BankAccount" runat="server" Text="Account" 
                            AutoPostBack="True" Checked="True" 
                            oncheckedchanged="Chk_BankAccount_CheckedChanged" style="color: #3399FF" />
                    </td>
                </tr>
                 <tr>
                    <td width="20%" colspan="2" style="width: 50%; text-align: right;">
                        Select Bank</td>
                    <td width="20%" style="text-align: left">
                        <asp:DropDownList ID="ddl_BankName" runat="server"  Width="149px" 
                            onselectedindexchanged="ddl_BankName_SelectedIndexChanged" TabIndex="8" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="20%" colspan="2" style="width: 50%; text-align: right;">
                        Branch Name</td>
                    <td width="20%" style="text-align: left">
                        <asp:DropDownList ID="ddl_branchname" runat="server"  Width="149px" 
                            AutoPostBack="True" 
                            onselectedindexchanged="ddl_branchname_SelectedIndexChanged"  >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="20%" colspan="2" style="width: 50%; text-align: right;">
                        Ifs Code</td>
                    <td width="20%" style="text-align: left">
                        <asp:DropDownList ID="ddl_Ifsc" runat="server"  Width="149px" 
                           TabIndex="9" AutoPostBack="True" 
                            onselectedindexchanged="ddl_Ifsc_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="20%" colspan="2" style="width: 50%; text-align: right;">
                        Agent AC-No 
                    </td>
                    <td width="20%" style="text-align: left">
                        <asp:TextBox ID="txt_AgentAccNo" runat="server"  Width="149px" TabIndex="13" />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="txt_AgentAccNo" validationExpression="^[0-9,.]{1,10}$" ErrorMessage="*"></asp:RequiredFieldValidator>

                    </td>
                </tr>
                
            </table>
            <table border="0" width="100%" id="table1" cellspacing="1">
                <tr>
                    <td width="20%">
                        <asp:DropDownList ID="ddl_Plantcode" runat="server" TabIndex="1" Width="20px"                           
                            AutoPostBack="True" ReadOnly="true" Visible="False" Height="16px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cmb_RouteID" runat="server" TabIndex="1" Width="20px" 
                            OnSelectedIndexChanged="cmb_RouteID_SelectedIndexChanged" 
                            AutoPostBack="True" ReadOnly="true" Visible="False" Height="16px">
                        </asp:DropDownList>
                    </td>
                    <td width="30%">
                        &nbsp;</td>
                    <td width="20%" align="right">
                        <asp:Button ID="btn_Addagent" runat="server" Text="Add"  BackColor="#6F696F"
                            ForeColor="White" Width="60px" TabIndex="10" 
        style="height: 26px" onclick="btn_Addagent_Click" />
                    </td>
                </tr>
                <tr>
                    <td width="20%">
                        &nbsp;</td>
                    <td width="30%">
                        &nbsp;</td>
                    <td width="20%" align="right">
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
        </asp:Panel>
            
                <br />
                <br />
                <table width="50%">
                    <tr>
                    <asp:Panel ID=PAN runat="server" Visible=false>
                        <td>

<fieldset align="center">
                    <legend style="color: #3399FF">SEARCH-AGENT</legend>
                    <table id="table2" border="0" cellspacing="1" width="100%">
                        <tr>
                            <td width="15%">
                            </td>
                            <td width="30%" style="text-align: right">
                                &nbsp;<span class="style3">Name</span>
                            </td>
                            <td width="35%" style="text-align: left">
                                <asp:TextBox ID="Agtxtsearch" runat="server" 
                                    OnDataBinding="Agtxtsearch_DataBinding" TabIndex="11" 
                                    Text='<%#Bind("Agent_Name")%>' />
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                    CompletionInterval="10" CompletionSetCount="12" EnableCaching="true" 
                                    MinimumPrefixLength="1" ServiceMethod="GetCompletionList" 
                                    ServicePath="AutoComplete.asmx" TargetControlID="Agtxtsearch">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td align="right">
                                <asp:Button ID="Button2" runat="server" BackColor="#6F696F" Font-Bold="False" 
                                    ForeColor="White" OnClick="Button2_Click" TabIndex="12" Text="Search" 
                                    Width="60px" />
                            </td>
                        </tr>
                    </table>
                    <table id="table3" border="0" cellspacing="1">
                        <tr>
                            <td width="20%">
                                &nbsp;</td>
                            <td align="right" width="20%">
                                <asp:Button ID="Button3" runat="server" BackColor="#6F696F" Font-Bold="False" 
                                    ForeColor="White" OnClick="Button3_Click" TabIndex="13" Text="Clear" 
                                    Width="60px" Height="26px" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                            </td>
                            </asp:Panel>
                    </tr>
                </table>
            
    </table>
   
 

    <center>
    <asp:Panel ID="HH" runat="server" Visible=false>
        <div class="grid">
            <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional" 
                Visible="false">
                <ContentTemplate>
                    <mcn:DataPagerGridView ID="gvProducts" runat="server" OnRowDataBound="RowDataBound"
                        AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" CssClass="datatable"
                        CellPadding="0" BorderWidth="0px" GridLines="None" DataSourceID="newagentgrid"
                        PageSize="5" onrowediting="gvProducts_RowEditing" 
                        onselectedindexchanged="gvProducts_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField HeaderText="AgentId" DataField="Agent_Id" 
                                SortExpression="Agent_Id">
                            </asp:BoundField>
                            <asp:BoundField DataField="Agent_Name" HeaderText="Agent_Name" 
                                SortExpression="Agent_Name" />                           
                            <asp:BoundField DataField="Plant_code" HeaderText="Plant_code" 
                                SortExpression="Plant_code" />
                                 <asp:BoundField DataField="Type" HeaderText="Type" 
                                SortExpression="Type" />
                            <asp:BoundField DataField="Cartage_Amt" HeaderText="CartageAmt" 
                                SortExpression="Cartage_Amt" />
                            <asp:BoundField DataField="Agent_AccountNo" HeaderText="AgentAccountNo" 
                                SortExpression="Agent_AccountNo" />
                            <asp:BoundField DataField="AddedDate" HeaderStyle-CssClass="first" 
                                HeaderText="AddedDate" ItemStyle-CssClass="first" SortExpression="AddedDate" />
                                
                        </Columns>
                        <PagerSettings Visible="False" />
                        <RowStyle CssClass="row" />
                    </mcn:DataPagerGridView>

           


                    <div class="pager">
                        <asp:DataPager ID="pager" runat="server" PageSize="8" PagedControlID="gvProducts">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹Previous"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                    NextPreviousButtonCssClass="command" />
                                <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="Next›"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                        
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </asp:Panel>
    </center>
    <br />
    <br />
    
    <asp:SqlDataSource ID="newagentgrid" runat="server" ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>"
        SelectCommand="SELECT [Agent_Id], [Agent_Name], [Plant_code], [Type], CAST([Cartage_Amt] AS DECIMAL(18,2)) AS [Cartage_Amt], [Agent_AccountNo], CONVERT(VARCHAR(10),[AddedDate],103) AS [AddedDate] FROM [Agent_Master] WHERE (([Company_code] = @Company_code) AND ([Plant_Code] = @Plant_Code) AND ([Route_id] = @Route_id)) ORDER BY Agent_Id DESC "
        FilterExpression="Agent_Name LIKE '%{0}%'">
        
        <SelectParameters>
            <asp:SessionParameter DefaultValue="Company_code" Name="Company_code" SessionField="Company_code"
                Type="Int32" />          
            <asp:SessionParameter DefaultValue="Plant_Code" Name="Plant_Code" 
                SessionField="Plant_Code" Type="String" />
            <asp:SessionParameter DefaultValue="Route_id" Name="Route_id" 
                SessionField="Route_ID" Type="Int32" />     
        </SelectParameters>
        
        <FilterParameters>
            <asp:ControlParameter Name="Agent_Name" ControlID="Agtxtsearch" PropertyName="Text" />
        </FilterParameters>
        
    </asp:SqlDataSource>           
 
    <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
</asp:Content>
