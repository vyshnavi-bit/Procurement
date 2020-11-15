<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Rpt_PlantwiseSingleBilldateOverAllData.aspx.cs" Inherits="Rpt_PlantwiseSingleBilldateOverAllData" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 9pt;
        }
        .Grid th
        {
            color: #fff;
            background-color: #3AC0F2;
            font-family: Arial, Helvetica, sans-serif;
        }
        /* CSS to change the GridLines color */
        .Grid, .Grid th, .Grid td
        {
            border: 1px solid #525252;
        }
        .panels
        {
            width: 100px;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
         .fontfam
        {
            font-family:Arial;
        }
        
        sansserif {
    font-family: Arial, Helvetica, sans-serif;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
<%--<asp:UpdateProgress ID="UpdateProgress" runat="server">
    <ProgressTemplate>
 <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" 
                AlternateText="Loading ..." ToolTip="Loading ..." 
                style="padding: 10px;position:fixed;top:38%; left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>
    <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div class="legagentsms">
                <fieldset class="fontt">
                    <legend style="color: #3399FF">Plant Procurement Report</legend>
                    <table border="0" width="100%" id="table4" cellspacing="1" align="center">
                    <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                            </td>
                            <td align="left">
                              
                                <asp:Label ID="Lbl_selectedReportItem" runat="server" visible="false"></asp:Label>
                              
                                <asp:RadioButtonList ID="rd_RportViewType" runat="server" 
                                    RepeatDirection="Horizontal" RepeatLayout="Table">
                                    <asp:ListItem Text="Horizontal" Value="Horizontal"></asp:ListItem>
                                    <asp:ListItem Text="Vertical" Value="Vertical"></asp:ListItem>                                
                                   
                                </asp:RadioButtonList>
                              
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                <asp:Label ID="lbl_frmdate" runat="server" Text="From"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;
                                <asp:TextBox ID="txt_FromDate" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" 
                                    PopupButtonID="txt_FromDate" PopupPosition="BottomRight" 
                                    TargetControlID="txt_FromDate">
                                </asp:CalendarExtender>
                                </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                <asp:Label ID="lbl_todate" runat="server" Text="To"></asp:Label>
                            </td>
                            <td align="left">
                                &nbsp;
                                <asp:TextBox ID="txt_ToDate" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" 
                                    PopupButtonID="txt_ToDate" PopupPosition="BottomRight" 
                                    TargetControlID="txt_ToDate">
                                </asp:CalendarExtender>
                                <asp:Button ID="btn_Load" runat="server" BackColor="#00CCFF" 
                                    BorderStyle="Double" Font-Bold="True" ForeColor="White" 
                                    onclick="btn_Load_Click" Text="Load" />
                                <br />
                               
                                </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                <asp:Label ID="lbl_ReportDisplayItems" runat="server" Text="ReportDisplayItems"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownCheckBoxes ID="ddchkCountry" runat="server" 
                                    AddJQueryReference="True" style="top: -30px; left: 0px" UseButtons="True" 
                                    UseSelectAllNode="True">
                                    <Style DropDownBoxBoxHeight="1000" 
                                        DropDownBoxBoxWidth="200" SelectBoxWidth="250" />
                                    <Texts SelectBoxCaption="Select Report DisplayItems" />
                                </asp:DropDownCheckBoxes>
                                &nbsp;
                               
                                
                                </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                            </td>
                            <td align="left">
                                &nbsp;
                                <asp:Button ID="btn_ok" runat="server" BackColor="#00CCFF" BorderStyle="Double" 
                                    Font-Bold="True" ForeColor="White" OnClick="btn_ok_Click" Text="OK" 
                                    Width="35px" />
                                <asp:Button ID="btn_print" runat="server" BackColor="#00CCFF" 
                                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                                    OnClientClick="return PrintPanel();" Text="Print" />
                                 <asp:Button ID="Button1" runat="server" BackColor="#00CCFF" BorderStyle="Double" 
                                    Font-Bold="True" ForeColor="White" OnClick="Button1_Click" Text="Export" 
                                    Width="50px" />
                                </td>
                        </tr>
                    </table>
                    <asp:Label ID="Lbl_Errormsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                    <br />
                </fieldset>
            </div>
            <div align="center">
                                <asp:Panel ID="Panel3" runat="server">
                        <asp:Table ID="Table2" runat="Server" BorderColor="White" BorderWidth="1" CellPadding="1"
                            CellSpacing="1" CaptionAlign="Top" Height="40px" Width="600px" >
                            <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1" Width="600px">
                                <asp:TableCell ID="TableCell22" runat="Server" BorderWidth="1">
                                    <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" CellPadding="1"
                                        CellSpacing="1" Width="600px" CaptionAlign="Top" Height="40px">
                                        <asp:TableRow ID="Title_TableRow" runat="Server" BorderWidth="1" BackColor="#3399CC"
                                            ForeColor="white" BorderColor="Silver">
                                            <asp:TableCell ID="TableCell6" runat="Server" BorderWidth="0">
                                                <asp:CheckBox ID="MChk_PlantName" Text="PlantName" runat="server" Checked="true" 
                                                    AutoPostBack="True" OnCheckedChanged="MChk_PlantName_CheckedChanged" />
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell2" runat="Server" BorderWidth="0">
                                                <asp:CheckBox ID="MChk_Date1" Text="Date1" runat="server" Checked="true" AutoPostBack="True"
                                                    OnCheckedChanged="MChk_Date1_CheckedChanged" />
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell3" runat="Server" BorderWidth="0">
                                                <asp:CheckBox ID="MChk_Date2" Text="Date2" runat="server" Checked="true" AutoPostBack="True" 
                                                    OnCheckedChanged="MChk_Date2_CheckedChanged" />
                                            </asp:TableCell>                                 
                                        </asp:TableRow>
                                        <asp:TableRow ID="TableRow2" runat="Server" BorderWidth="0" BorderColor="Silver"
                                            BackColor="#ffffec">
                                            <asp:TableCell ID="TableCell1" runat="Server" BorderWidth="0">
                                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderWidth="0">
                                                </asp:CheckBoxList>
                                            </asp:TableCell>
                                            <asp:TableCell ID="TableCell4" runat="Server" BorderWidth="0">
                                                <asp:CheckBoxList ID="CheckBoxList2" runat="server" BorderWidth="0">
                                                </asp:CheckBoxList>
                                            </asp:TableCell> 
                                            <asp:TableCell ID="TableCell5" runat="Server" BorderWidth="0">
                                                <asp:CheckBoxList ID="CheckBoxList3" runat="server" BorderWidth="0">
                                                </asp:CheckBoxList>
                                            </asp:TableCell>                                       
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                                  
                    </asp:Panel>                   
                    </div>
                  
            <br />
            <asp:Panel ID="pnlContents" runat="server">
              <div align="left">
       <asp:Image ID="Image1"  runat="server" ImageUrl="~/Image/VLogo.png" Width="50px" Height="35px" /> 
                  <asp:Label ID="Label1" runat="server" Text="SRI VYSHNAVI DAIRY SPECIALITIES (P) LIMITED" Font-Bold="true" Font-Size="12px"></asp:Label>  <br />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="ALL PLANT PROCUREMENT BILL REPORT" Font-Bold="true" Font-Size="11px"></asp:Label>        

                    <asp:GridView ID="gvCustomers" CssClass="fontfam" runat="server" AutoGenerateColumns="True"  
                      AllowPaging="True" HeaderStyle-BackColor="Silver"
                         PageSize="50" Font-Size="12px" Font-Bold="true">
                    </asp:GridView>
                    <br />
                </div>
            </asp:Panel>
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
