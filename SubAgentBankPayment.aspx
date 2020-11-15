<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SubAgentBankPayment.aspx.cs" Inherits="SubAgentBankPayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style4
        {
            height: 24px;
            text-align: left;
        }
    </style>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .selected
        {
            background-color: #A1DCF2;
        }
        .style5
        {
            width: 100%;
        }
    </style>
    <%--
                                           <asp:Button ID="GetSelectedRecords" runat="server" 
                                               onclick="GetSelectedRecords_Click" Text="Load" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[id*=chkHeader]").live("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=chkRow]").live("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form>
    <body>
        <html>
        <div>
            <center>
                <br />
                <asp:Label ID="Label111" runat="server" Text="Sub Agent Amount Allotment" Style="font-family: serif;
                    font-size: medium;"></asp:Label>
                <table class="style5">
                    <tr width="50%">
                        <td align="left">
                <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/SubAgentsMaster.aspx" 
                                Visible="False">Sub Agent Master</asp:LinkButton>
                        </td>
                        <td align="right">
                <asp:LinkButton ID="LinkButton1" runat="server" 
                                PostBackUrl="~/SubAgentBankFIle.aspx" Visible="False">Bank Payment Select list</asp:LinkButton>
                        </td>
                    </tr>
                    </table>
                <br />
                <br />
            </center>
        </div>
        <div align="center">
            <center style="border-style: none">
                <asp:Panel ID="Panel1" Width="30%" runat="server" BorderWidth="1px">
                    <center>
                        <table width="100%" cellspacing="0">
                            <tr  width=50%>
                                <td width=50% style="text-align: right">
                                    <asp:Label ID="Plantname13" runat="server" Style="font-family: serif" Text="PlantName"></asp:Label>
                                </td>
                                <td class="style4">
                                    <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" CssClass="tb10"
                                        Font-Bold="True" OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged"
                                        Width="200px" Font-Size="12px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="text-align: right" >
                                    <asp:Label ID="Plantname22" runat="server" Style="font-family: serif" 
                                        Text="Agent Name"></asp:Label>
                                </td>
                                <td class="style4">
                                    <asp:DropDownList ID="ddl_getagent" runat="server" AutoPostBack="True" 
                                        CssClass="tb10" Font-Bold="True" Font-Size="12px" Width="200px" 
                                        onselectedindexchanged="ddl_getagent_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr align="left">
                                <td style="text-align: right">
                                    <asp:Label ID="Plantname20" runat="server" Style="font-family: serif" 
                                        Text="Total Milk Amount"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_milkamount" runat="server" CssClass="tb10" Enabled="False" 
                                        Height="25px" OnTextChanged="txt_tot_TextChanged" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <div>
                                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Load" />
                                        <%--
                                           <asp:Button ID="GetSelectedRecords" runat="server" 
                                               onclick="GetSelectedRecords_Click" Text="Load" />--%>
                                        <asp:Button ID="btnchenck" runat="server" backcolor="Green" 
                                            borderstyle="Inset" font-bold="True" forecolor="White" height="26px" 
                                            onclick="btnchenck_Click"  text="Check" width="60px" xmlns:asp="#unknown" />
                                        <asp:Button ID="GetSelectedRecords" runat="server" backcolor="Green" 
                                            borderstyle="Inset" font-bold="True" forecolor="White" height="26px" 
                                            onclick="GetSelectedRecords_Click"  text="Save" width="60px"  />
                                        </asp:button>
                                    </div>
                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="2" style="text-align: center">
                                    <asp:Label ID="Label11" runat="server" 
                                        Style="font-family: serif; font-size: medium; color: #660033; font-weight: 700;" 
                                        Visible="False"></asp:Label>
                                    <asp:Label ID="avil" runat="server" 
                                        Style="font-family: serif; font-size: medium; color: #660033; font-weight: 700;" 
                                        Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="2">
                                    <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" Enabled="true" Height="25px"
                                        Width="50px" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txt_fileName123" runat="server" CssClass="tb10" Enabled="true" Height="25px"
                                        Width="142px" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txt_ToDate" runat="server" CssClass="tb10" Enabled="true" Height="25px"
                                        Width="50px" Visible="False"></asp:TextBox>
                                    <asp:TextBox ID="txt_check" runat="server" CssClass="tb10" Enabled="true" 
                                        Height="25px" Visible="False" Width="50px"></asp:TextBox>
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </center>
                </asp:Panel>
                <br />
                <table class="style5">
                    <tr>
                        <td align="center">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"   
                                            FooterStyle-BackColor="#3AC0F2" FooterStyle-Font-Bold="true" 
                                            FooterStyle-ForeColor="Tomato" HeaderStyle-BackColor="#3AC0F2" 
                                            HeaderStyle-ForeColor="White"> 
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNO" ItemStyle-Width="50">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                  <%-- <asp:BoundField DataField="Agent_id" HeaderText="AgentId" ItemStyle-Width="50">
                                                    <ItemStyle Width="50px" />
                                                </asp:BoundField>--%>
                                                <asp:BoundField DataField="Agent_Name" HeaderText="AgentName" 
                                                    ItemStyle-Width="50">
                                                    <ItemStyle Width="50px" />
                                                </asp:BoundField>
                                                
                                                <asp:BoundField DataField="Bank_Name" HeaderText="BankName" 
                                                    SortExpression="Bank_Name" />
                                                <asp:BoundField DataField="ifsc_code" HeaderText="ifsccode" 
                                                    SortExpression="ifsc_code" />
                                                <asp:BoundField DataField="Agent_accountNo" HeaderText="AccountNo" 
                                                    SortExpression="Agent_accountNo" />
                                                
                                                <asp:TemplateField HeaderText="EditAmount">
                                                    <ItemTemplate>
                                                       <asp:TextBox ID="txtchkamount" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRow" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#3AC0F2" Font-Bold="True" ForeColor="Tomato" />
                                            <HeaderStyle BackColor="#3AC0F2" ForeColor="White" />
                                        </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </center>
        </div>
        <left>
            <asp:Panel ID="Panel2" runat="server" Width="60%">
            </asp:Panel>
        </left>
        <div align="center">
            <br />
            <br />
        </div>
    </form>
    </body> </html>
    </asp:Content>