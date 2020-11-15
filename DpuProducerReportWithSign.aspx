<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DpuProducerReportWithSign.aspx.cs" Inherits="DpuProducerReportWithSign" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp"  %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
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



    .buttonclass
{
padding-left: 10px;
        font-weight: 700;
    }



    .style5
    {
        height: 22px;
    }
    .style6
    {
        background-image: url('style/images/form_bg.jpg');
        background-repeat: repeat-x;
        border: 1px solid #d1c7ac;
        color: #333333;
        padding: 3px;
        margin-right: 4px;
        margin-bottom: 8px;
        Font-Size: medium;
        font-family: Andalus;
        text-align: left;
    }
    .style7
    {
        font-weight: normal;
    }



    </style>


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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>



    <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height:10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
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
                    Text="Dpu Producer Signature"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="right">
                <asp:Label ID="Label4" runat="server" CssClass="style3" Text="Plant Name"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddl_PlantName" runat="server" Height="20px" 
                    onselectedindexchanged="ddl_PlantName_SelectedIndexChanged" 
                    AutoPostBack="True" Width="150px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="right">
                <asp:Label ID="lblagent" runat="server" CssClass="style4" Text="Agent Code"></asp:Label>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddl_Agentname" runat="server" AutoPostBack="True" 
                    CssClass="style7" Height="20px" 
                    onselectedindexchanged="ddl_Agentname_SelectedIndexChanged" Width="150px">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="Label5" runat="server" CssClass="style4" Text="From Date"></asp:Label>
                <asp:TextBox ID="txt_FromDate" runat="server" Font-Size="Small"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                    TargetControlID="txt_FromDate">
                </asp:CalendarExtender>
                <asp:Label ID="Label6" runat="server" CssClass="style4" Text="ToDate"></asp:Label>
                <asp:TextBox ID="txt_ToDate" runat="server" Font-Size="Small"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                    PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                    TargetControlID="txt_ToDate">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td colspan="4">

                        <asp:DropDownList ID="ddl_agentid" runat="server" 
                            CssClass="style6" Font-Size="X-Small" Height="20px" Visible="False" 
                            Width="70px">
                        </asp:DropDownList>

                        <asp:DropDownList ID="ddl_Plantcode" runat="server" CssClass="style6" 
                            Font-Size="X-Small" Height="20px" Visible="False" Width="70px">
                        </asp:DropDownList>

                        <asp:Button ID="btn_ok" runat="server" 
                            Font-Bold="True" ForeColor="#333300" onclick="btn_ok_Click" Text="Ok" 
                            CssClass="buttonclass" Height="25px" Width="50px" />
                        <asp:Button ID="Button3" runat="server" CssClass="buttonclass" 
                            onclick="Button3_Click" Text="Export" />

                            <asp:Button ID="Button1" runat="server"
        Font-Bold="True" Height="26px" OnClientClick="return PrintPanel();"
        Text="Print" CssClass="buttonclass" />           
                        <br />
            </td>
        </tr>
        </table>
    </fieldset>
    <br />
                        <asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label>
    <br />
    <table class="style2">
        <tr valign="top">

            <td>
                <br />
                <asp:Panel ID="pnlContents" runat="server" Height="411px">
                    <table  width=100% style="height: 403px">
                        <tr valign=top>
                            <td align="left">
                                <asp:Image ID="Image1" runat="server" Height="40px" 
                                    ImageUrl="~/Images/VLogo.png" Width="40px" />
                            </td>
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="GridView1" runat="server" BackColor="White" 
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                        Font-Size="14px" onrowcreated="GridView1_RowCreated" 
                                        onrowdatabound="GridView1_RowDataBound" ShowFooter="true">
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </td>
                                </caption>
                            </tr>
                        </tr>
                    </table>
                </asp:Panel>
                <br />
                <br />
            </td>

        </tr>
    </table>
    <br />
</center>
</div>

 </ContentTemplate>

   <Triggers>
<asp:PostBackTrigger ControlID="Button3" />
</Triggers>

        </asp:UpdatePanel>
        

        </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
