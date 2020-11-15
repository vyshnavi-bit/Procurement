<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Rpt_PlantpermonthOverAllData.aspx.cs" Inherits="Rpt_PlantpermonthOverAllData" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
  <asp:UpdateProgress ID="UpdateProgress" runat="server">
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
        <ContentTemplate>
    <table border="0" width="100%" id="tab" cellspacing="1" align="center" CaptionAlign="Top">
    <tr>
    <td width="50%">
      <div align="center">
       <asp:Label ID="Lbl_MilkTypeType" runat="server" visible="false"></asp:Label>
                     <asp:RadioButtonList ID="rd_MilkTypeType" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Table" AutoPostBack="True" 
                            onselectedindexchanged="rd_MilkTypeType_SelectedIndexChanged">
                    <asp:ListItem Text="Cow" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Buffalo" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            <asp:Panel ID="Panel3" runat="server">
                <asp:Table ID="Table2" runat="Server" BorderColor="White" BorderWidth="1" CellPadding="1"
                    CellSpacing="1" CaptionAlign="Top" Height="40px" Width="200px" Style="margin-left: 0px">
                    <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1" Width="250px">
                        <asp:TableCell ID="TableCell22" runat="Server" BorderWidth="1">
                            <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" CellPadding="1"
                                CellSpacing="1" Width="250px" CaptionAlign="Top" Height="40px">
                                <asp:TableRow ID="Title_TableRow" runat="Server" BorderWidth="1" BackColor="#3399CC"
                                    ForeColor="white" BorderColor="Silver">
                                    <asp:TableCell ID="TableCell6" runat="Server" BorderWidth="0">
                                        <asp:CheckBox ID="MChk_PlantName" Text="PlantName" runat="server" Checked="true"
                                            AutoPostBack="True" OnCheckedChanged="MChk_PlantName_CheckedChanged" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow2" runat="Server" BorderWidth="0" BorderColor="Silver"
                                    BackColor="#ffffec">
                                    <asp:TableCell ID="TableCell1" runat="Server" BorderWidth="0">
                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderWidth="0">
                                        </asp:CheckBoxList>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
      </div>
    </td>
    <td width="50%">
    
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td align="right">
                        &nbsp;</td>
                    <td>
                    
                    </td>
                    <td align="left">
                <asp:Label ID="Lbl_selectedReportItem" runat="server" visible="false"></asp:Label>
                <asp:RadioButtonList ID="rd_RportViewType" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Table">
                    <asp:ListItem Text="COSTING" Value="Horizontal"></asp:ListItem>
                    <asp:ListItem Text="TS/FKGRATE" Value="Vertical"></asp:ListItem>
                </asp:RadioButtonList>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label2" runat="server" Text="From"></asp:Label>
                    </td>
                    <td align="right">
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_FromDate" runat="server" Enabled="true"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                            PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                                
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                       
                        <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td align="left">
                                
                        <asp:TextBox ID="txt_ToDate" runat="server" Enabled="true"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                            PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                                
                    </td>
                    <td width="12%">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        &nbsp;
                        <asp:Label ID="lbl_ReportDisplayItems" runat="server" Text="ReportDisplayItems"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td align="left">
                     
                       
                     
                        <asp:DropDownCheckBoxes ID="ddchkCountry" runat="server" 
                            AddJQueryReference="True" style="top: -30px; left: 0px" UseButtons="True" 
                            UseSelectAllNode="True">
                            <Style DropDownBoxBoxHeight="1000" 
                                DropDownBoxBoxWidth="200" SelectBoxWidth="250" />
                            <Texts SelectBoxCaption="Select Report DisplayItems" />
                        </asp:DropDownCheckBoxes>
                        <asp:DropDownCheckBoxes ID="BuffDisplayItems" runat="server" 
                            AddJQueryReference="True" style="top: -30px; left: 0px" UseButtons="True" 
                            UseSelectAllNode="True">
                            <Style DropDownBoxBoxHeight="1000" 
                                DropDownBoxBoxWidth="200" SelectBoxWidth="250" />
                            <Texts SelectBoxCaption="Select Report DisplayItems" />
                        </asp:DropDownCheckBoxes>
                     
                       
                     
                    </td>
                    <td width="12%">
                    </td>
                </tr>
                 <tr>
                    <td>
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td>
                    </td>
                    <td align="left">
                                
                                    <asp:Button ID="btn_Get" runat="server" BackColor="#00CCFF" 
                                        BorderStyle="Double" Font-Bold="True" ForeColor="White" OnClick="btn_Get_Click" 
                                        Text="Get" Height="28px" />
                                    <asp:Button ID="btnPrintCurrent" runat="server" BackColor="#00CCFF" 
                                        BorderStyle="Double" Font-Bold="True" ForeColor="White" 
                                        OnClientClick="return PrintPanel();" Text="Print" />
                     </td>
                    <td width="12%">
                    </td>
                </tr>
                 <tr>
                    <td>
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td>
                    </td>
                    <td align="left">
                    <asp:Label ID="Lbl_Errormsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
                     </td>
                    <td width="12%">
                    </td>
                </tr>
            </table>  
    </td>
    </tr>
    </table> 
     <br />
            <asp:Panel ID="pnlContents" runat="server">
              <div align="left">
       <asp:Image ID="Image1"  runat="server" ImageUrl="~/Image/VLogo.png" Width="50px" Height="35px" /> 
                  <asp:Label ID="Label1" runat="server" Text="SRI VYSHNAVI DAIRY SPECIALITIES (P) LIMITED" Font-Bold="true" Font-Size="12px"></asp:Label>  <br />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Lbt_Title" runat="server" Text="ALL PLANTWISE MONTHLY PROCUREMENT BILL REPORT" Font-Bold="true" Font-Size="11px"></asp:Label> &nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="From" Font-Bold="true" Font-Size="11px"></asp:Label><asp:Label ID="Label5" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label><asp:Label ID="Label6" runat="server" Text="To" Font-Bold="true" Font-Size="11px"></asp:Label><asp:Label ID="Label7" runat="server"  Font-Bold="true" Font-Size="11px"></asp:Label>                                
                  
                    <asp:GridView ID="gvCustomers" CssClass="fontfam" runat="server" AutoGenerateColumns="True" Font-Bold="true"
                      AllowPaging="True" HeaderStyle-BackColor="Silver"
                         PageSize="50" Font-Size="12px" >
                    </asp:GridView>
                    <br />
                </div>
            </asp:Panel>
              </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

