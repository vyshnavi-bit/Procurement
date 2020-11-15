<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CompareDpuProcureData.aspx.cs" Inherits="CompareDpuProcureData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

.style1
{
    width:500px;
    text-align:center;
    background-color:Gray;
    
    
}



    .style2
    {
        width: 100%;
    }
    .style3
    {
        font-family: Andalus;
        color: #000000;
    }



    .style4
    {
        color: #000000;
        font-family: serif;
    }



    .style6
    {
        color: #000000;
    }



        .gridview1
        {
            font-family: Andalus;
        }



    .style7
    {
        font-family: serif;
    }
        .buttonclass
        {
            font-weight: 700;
            height: 26px;
        }
        .style8
    {
        height: 42px;
    }



    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>



    <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height:1%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>


<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>





<div>
<center>
    <br />
<fieldset class ="style1"> 
    <br />
    <table class="style2" bgcolor="#99FFCC">
        <tr>
            <td colspan="4">
                <asp:Label ID="Label7" runat="server" CssClass="style3" 
                    Text="Dpu Milk Camparison"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="25%">
                &nbsp;</td>
            <td align="right">
                <asp:Label ID="Label4" runat="server" CssClass="style3" Text="Plant Name"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddl_PlantName" runat="server" Height="25px" 
                    CssClass="tb10" Font-Size="Small">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style8">
                </td>
            <td align="right" class="style8">
                <asp:Label ID="Label5" runat="server" CssClass="style4" Text="From Date"></asp:Label>
            </td>
            <td align="left" class="style8">
                <asp:TextBox ID="txt_FromDate" runat="server" Font-Size="Small" CssClass="tb10"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                    TargetControlID="txt_FromDate">
                </asp:CalendarExtender>
            </td>
            <td class="style8">
                </td>
        </tr>
        <tr align="center">
            <td colspan="4">
                <asp:RadioButtonList ID="Rdolistperiod" runat="server" 
                    RepeatDirection="Horizontal" style="font-family: serif">
                    <asp:ListItem Value="1">Per Day</asp:ListItem>
                    <asp:ListItem Value="2">AM</asp:ListItem>
                    <asp:ListItem Value="3">PM</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr align="center">
            <td colspan="4">
                <asp:CheckBox ID="CheckBox1" runat="server" 
                    oncheckedchanged="CheckBox1_CheckedChanged" style="font-family: serif" 
                    Text="Click To Local Import" AutoPostBack="True" />
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    RepeatDirection="Horizontal" style="font-family: Andalus; height: 32px">
                    <asp:ListItem Value="1">Live Data </asp:ListItem>
                    <asp:ListItem Value="2">Excel Data</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Button ID="Save" runat="server" CssClass="buttonclass" 
                    onclick="Button1_Click" OnClientClick="return validate();" Text="Import" />
            </td>
        </tr>
        <tr align="center">
            <td colspan="4">
                &nbsp;</td>
        </tr>
        <tr align="center">
            <td colspan="4">
                &nbsp;&nbsp;&nbsp;<asp:Button ID="btn_ok" runat="server" BackColor="#FFFF99" 
                    BorderStyle="Double" CssClass="button2222" Font-Bold="True" ForeColor="#333300" 
                    Height="30px" onclick="btn_ok_Click" Text="Show" />
                &nbsp;&nbsp;
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="4">

                        <asp:Label ID="Label6" runat="server" CssClass="style4" Text="ToDate" 
                            Visible="False"></asp:Label>

                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" 
                            Visible="False" />

                        <asp:DropDownList ID="ddl_AgentId" runat="server" Font-Bold="True" 
                            Font-Size="Large" Visible="False">
                        </asp:DropDownList>
                        <asp:Label ID="lblagent" runat="server" CssClass="style4" Text="Agent Code" 
                            Visible="False"></asp:Label>
                        <asp:CheckBox ID="Chk_single" runat="server" AutoPostBack="True" 
                            CssClass="style6" oncheckedchanged="Chk_single_CheckedChanged" 
                            style="font-family: serif" Text="Single" Visible="False" />
                        <asp:TextBox ID="txt_ToDate" runat="server" Font-Size="Small" Visible="False"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                            TargetControlID="txt_ToDate">
                        </asp:CalendarExtender>
                        <br />
            </td>
        </tr>
        </table>
    </fieldset>
    <br />
                        <asp:Label ID="lblmsg" runat="server" Text="Label" 
        CssClass="style7"></asp:Label>
    <br />
    <asp:Label ID="lblpcode" runat="server" Text="Label" CssClass="style7"></asp:Label>
    <br />
    <br />
    <table class="style2">
        <tr valign="top">
            <td align="center" width=33% >
                <asp:Label ID="livedata" runat="server" Text="Live Data" 
                    style="color: #339933"></asp:Label>
            </td>
            <td align="center" width=33%>
                <asp:Label ID="Exceldata" runat="server" style="color: #FF9900" 
                    Text="Excel Data"></asp:Label>
            </td>
            <td align="center" width=33%>
                <asp:Label ID="receivedata" runat="server" style="color: #FF0000" 
                    Text="Received Data"></asp:Label>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" width="33%">
                <asp:Label ID="lbldpunos" runat="server" style="color: #339933" 
                    Text="Live Data"></asp:Label>
                <br />
                 <asp:GridView ID="GridView1" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                                AutoGenerateColumns="false" 
                    CssClass="gridview2" Font-Size="10px" >
                      <Columns>
                                    <asp:TemplateField HeaderText="SNO" ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="agent_code" HeaderText="AgentId" 
                                        SortExpression="agent_code">
                                    <ItemStyle Width="20px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="producer_code" HeaderText="prodcode" 
                                        SortExpression="producer_code">
                                    <ItemStyle Width="20px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Fat" HeaderText="Fat" SortExpression="Fat" />
                                    <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" />
                                    <asp:BoundField DataField="Milk_kg" HeaderText="MilkKg" 
                                        SortExpression="Milk_kg" />
                                    </Columns>
                      <FooterStyle BackColor="#3AC0F2" Font-Bold="True" ForeColor="Tomato" />
                      <HeaderStyle BackColor="#3AC0F2" ForeColor="White" />
                </asp:GridView>
            </td>
            <td align="center" width="33%">
                <asp:Label ID="lbldpunos1" runat="server" style="color: #FF9900" 
                    Text="Excel Data"></asp:Label>
                <asp:GridView ID="GridView3" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                                AutoGenerateColumns="false"
                    CssClass="gridview2" Font-Size="10px" >
                    <Columns>
                        <asp:TemplateField HeaderText="SNO" ItemStyle-Width="50">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="agent_code" HeaderText="AgentId" 
                            SortExpression="agent_code">
                        <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="producer_code" HeaderText="prodcode" 
                            SortExpression="producer_code">
                        <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Fat" HeaderText="Fat" SortExpression="Fat" />
                        <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" />
                        <asp:BoundField DataField="Milk_kg" HeaderText="MilkKg" 
                            SortExpression="Milk_kg" />
                    </Columns>
                </asp:GridView>
             
            </td>
            <td align="center" width="33%">
                <asp:Label ID="lbldputrandnos" runat="server" style="color: #FF0000" 
                    Text="Received Data"></asp:Label>
                <br />
                <asp:GridView ID="GridView2" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                                AutoGenerateColumns="false" 
                             
                    CssClass="gridview2" Font-Size="10px" >
                    <Columns>
                        <asp:TemplateField HeaderText="SNO" ItemStyle-Width="50">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="agent_code" HeaderText="AgentId" 
                            SortExpression="agent_code">
                        <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="producer_code" HeaderText="prodcode" 
                            SortExpression="producer_code">
                        <ItemStyle Width="20px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Fat" HeaderText="Fat" SortExpression="Fat" />
                        <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" />
                        <asp:BoundField DataField="Milk_kg" HeaderText="MilkKg" 
                            SortExpression="Milk_kg" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr valign="top">
            <td align="center">
                <center>
                </center>
                <br />
            </td>
            <td align="center">
                &nbsp;</td>
            <td align="center">
                <center>
                </center>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <br />
    <br />
</center>
</div>

 </ContentTemplate>

        </asp:UpdatePanel>
        

        </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

