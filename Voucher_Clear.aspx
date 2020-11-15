<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Voucher_Clear.aspx.cs" Inherits="Voucher_Clear" Title="OnlineMilkTest|Voucher Clearing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />
    <style>
        .text1
        {
            border: 2px solid rgb(173, 204, 204);
            height: 21px;
            width: 200px;
            box-shadow: 0 0 27px rgb(204, 204, 204) inset;
            padding: 3px 3px 3px 3px;
            text-align: center;
        }
        
        .button
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            background-color: #4CAF50; /* Green */
            color: white;
            padding: 10px 26px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: medium;
            margin: 4px 2px;
            cursor: pointer;
            font-weight: 700;
        }
        
        
        
.text2 {
    border: 2px solid rgb(173, 204, 204);
    height: 31px;
    width: 100%;
    box-shadow: 0 0 27px rgb(204, 204, 204) inset;
    transition: 500ms all ease;
    padding: 3px 3px 3px 3px;
        text-align: left;
    }

.text2:hover,
.text2:focus {
    width:100%;
    transition: 500ms all ease;
  
    background-size: 25px 25px;
    background-position: 96% 62%;
    padding: 3px 32px 3px 3px;
}
        .style1
        {
            width: 50%;
            height: 48px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                <tr>
                    <td width="100%" colspan="2">
                        <br />
                        <p  style="line-height: 150%;">
                            &nbsp;&nbsp;CASH VOUCHER ENTRY</p>
                    </td>
               <%-- </tr>
               
                  
                <tr>--%>
                    <td width="100%" class="line" height="1px" colspan="2">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="width: 100%;" align="center">
        <table width="50%" align="center">
            <tr>
                <td width="40%" align="right">
            </td>
                <td width="40%" align="right">
                    <asp:Label ID="Label13" runat="server" Font-Size="Medium" Text="Clearing_Date"></asp:Label>
                </td>
                <td width="60%" class="fontt">
                    <asp:TextBox ID="txt_ClearingDate" runat="server" class="text1"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_ClearingDate"
                        PopupButtonID="txt_ClearingDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                    </asp:CalendarExtender>
                </td>
               <%-- <td width="10%">
                    &nbsp;
                </td>--%>
              <td width="40%" align="right">
                    <asp:Label ID="Label12" runat="server" Font-Size="Medium" Text="Inward_Date"></asp:Label>
                </td>
               <td width="60%" class="fontt">
                    <asp:TextBox ID="txt_InwardDate" runat="server" TabIndex="1" class="text1"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_InwardDate"
                        PopupButtonID="txt_InwardDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                    </asp:CalendarExtender>
                </td>
                <td width="12%">
                    &nbsp;
                </td>
                <td width="20%">
                </td>
            </tr>
        <%--</table>
        <table width="100%">--%>
            <tr>
            <td width="40%" align="right">
            </td>
                <td width="40%" align="right">
                    <asp:Label ID="Label4" runat="server" Font-Size="Medium" Text="Plant Name"></asp:Label>
                </td>
                <td width="60%" class="fontt">
                    <asp:DropDownList ID="ddl_Plantname" AutoPostBack="true" runat="server" 
                        Font-Size="Large" Height="30px" OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged"
                        Width="200px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" Width="230px"
                        Visible="false" OnSelectedIndexChanged="ddl_Plantcode_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td width="40%" align="right">
                    <asp:Label ID="Label1" runat="server" Font-Size="Medium" Text="RouteName"></asp:Label>
                </td>
                <td width="60%" class="fontt">
                    <asp:DropDownList ID="cmb_RouteName" AutoPostBack="true" runat="server" 
                        Width="170px" OnSelectedIndexChanged="cmb_RouteName_SelectedIndexChanged" Style="font-size: Large;
                        font-weight: bold; height: 30px; width: 200px;">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmb_RouteID" AutoPostBack="true" runat="server" Width="170px"
                        Visible="false">
                    </asp:DropDownList>
                </td>
            </tr>
        <%--</table>
        <table width="100%">--%>
            <tr>
                 <td width="40%" align="right">
            </td>
                <td width="40%" align="right">
                    <asp:Label ID="Label2" runat="server" Font-Size="Medium" Text="AgentName"></asp:Label>
                </td>
                <td width="60%" class="fontt">
                    <asp:DropDownList ID="ddl_AgentName" AutoPostBack="true" runat="server" Width="170px"
                        OnSelectedIndexChanged="ddl_AgentName_SelectedIndexChanged"  Style="font-size: Large;
                        font-weight: bold; height: 30px; width: 200px;">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddl_AgentId" AutoPostBack="true" runat="server" Width="170px"
                        Visible="false">
                    </asp:DropDownList>
                </td>
                <td width="40%" align="right">
                    <asp:Label ID="Label3" runat="server" Font-Size="Medium" Text="Session"></asp:Label>
                </td>
                <td width="60%" class="fontt">
                    <asp:DropDownList ID="cmb_session" runat="server"  Style="font-size: Large;
                        font-weight: bold; height: 30px; width: 200px;" AppendDataBoundItems="True" AutoPostBack="True"
                        Width="170px">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>AM</asp:ListItem>
                        <asp:ListItem>PM</asp:ListItem>
                    </asp:DropDownList>
                </td>
     <%--       </tr>
        </table>
        <table width="100%">--%>
            <%--  </table>
        <table width="100%">--%>
            <tr>
             <td width="40%" align="right">
            </td>
                <td width="40%" align="right">
                    <asp:Label ID="Label5" runat="server" Font-Size="Medium" Text="Milk_Ltr"></asp:Label>
                </td>
                <td width="60%" class="fontt">
                    <asp:TextBox ID="txt_Mltr" runat="server" class="text1"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                        ControlToValidate="txt_Mltr" ErrorMessage="Numeric only..." ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                </td>
                <td width="40%" align="right">
                    <asp:Label ID="Label8" runat="server" Font-Size="Medium" Text="Clr"></asp:Label>
                </td>
                <td width="60%" class="fontt">
                    <asp:TextBox ID="txt_clr" runat="server" class="text1" readonly></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                        ControlToValidate="txt_clr" ErrorMessage="Numeric only..." ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
             <td width="40%" align="right">
            </td>
                <td width="40%" align="right">
                    <asp:Label ID="Label6" runat="server" Font-Size="Medium" Text="Fat"></asp:Label>
                </td>
                <td width="60%" class="fontt">
                    <asp:TextBox ID="txt_Fat" runat="server" class="text1"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
                        ControlToValidate="txt_Fat" ErrorMessage="Numeric only..." ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                </td>
                <td width="40%" align="right">
                    <asp:Label ID="Label7" runat="server" Font-Size="Medium" Text="Snf"></asp:Label>
                </td>
                <td width="60%" class="fontt">
                    <asp:TextBox ID="txt_Snf" runat="server" OnTextChanged="txt_Snf_TextChanged" class="text1"
                        AutoPostBack="true"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server"
                        ControlToValidate="txt_Snf" ErrorMessage="Numeric only..." ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
             <td width="40%" align="right">
            </td>
                
                <td width="40%" align="right">
                    <asp:Label ID="Label9" runat="server" Font-Size="Medium" Text="Rate"></asp:Label>
                </td>
                <td width="60%" class="fontt">
                    <asp:TextBox ID="txt_Rate" runat="server" OnTextChanged="txt_Rate_TextChanged" class="text1"
                        AutoPostBack="true"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server"
                        ControlToValidate="txt_Rate" ErrorMessage="Numeric only..." ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                </td>
                <td width="40%" align="right">
                    <asp:Label ID="Label10" runat="server" Font-Size="Medium" Text="Amount"></asp:Label>
                </td>
                <td width="60%" class="fontt">
                    <asp:TextBox ID="txt_Amount" runat="server" class="text1"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                        ControlToValidate="txt_Amount" ErrorMessage="Numeric only..." ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
             <td width="40%" align="right">
            </td>
                
                <td width="40%" align="right">
                    <asp:Label ID="Label11" runat="server" Font-Size="Medium" Text="Remarks"></asp:Label>
                </td>
                <td width="60%" class="fontt">
                    <asp:TextBox ID="txt_Remarks" Width="250px" class="text1" runat="server"></asp:TextBox>
                </td>
            </tr>
            </table>
        </div>
        <table width="100%">
            <tr align="center">
                <td width="42%" class="style1">
                    <asp:Button ID="btn_Save" runat="server" CssClass="button" OnClick="btn_Save_Click"
                        Text="Save" Font-Size="10px" Font-Bold="True" />
                </td>
            </tr>
          </table>
          <table class="text2" align=center>
            <tr align="center">

                <td width="42%" class="style1">
                    <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover"
                        runat="server" PageSize="20" Font-Size="Medium" OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnRowCancelingEdit="GridView1_RowCancelingEdit" OnSelectedIndexChanging="GridView1_SelectedIndexChanging"
                        OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle ForeColor="#660066" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
</asp:Content>
