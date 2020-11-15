<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AgentDetailsMaintains.aspx.cs" Inherits="AgentDetailsMaintains" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .perdaymilkstatus
{
    background-color:#FFFAFA;
    margin-left:80px;
    width:700px;
}
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
        .modalPopupA
        {
            background-color: #FFFFFF;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
        .fontfam
        {
            font-family: Arial;
        }
        
        sansserif
        {
            font-family: Arial, Helvetica, sans-serif;
        }
        .DText
    {
width: 180px;
height: 25px;
border-radius: 5px;
padding: 2px;

}      
    
    .style1
    {
        height: 17px;
    }
    .style2
    {
        height: 32px;
    }
    .style3
    {
        color: #3399FF;
    }
    .style4
    {
        height: 17px;
        color: #3399FF;
    }
    .modalBackgroundA
{
background-color: White;
}

.modalPopupA
{
background-color: #FFFFFF;
filter: alpha(opacity=40);
opacity: 0.7;
xindex:-1;
}

.modalBackground
    {
        background-color: #3c454f;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }
    .modalPopup
    {
        background-color: #FFFFFF;       
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding:0
      
    }
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 6px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
        border-radius: 4px;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }


/*----------------data Table GridView Css----------------------*/
        .EU_TableScroll
        {
      /*----------max-height: 300px;-------------------*/       
            overflow: auto;
            border:1px solid #ccc;
           
        }
        .EU_DataTable
        {
            border-collapse: collapse;
            width:100%;
        }
            .EU_DataTable tr th
            {
                background-color: #3c454f;
                color: #ffffff;
                padding: 10px 5px 10px 5px;
                border: 1px solid #cccccc;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 12px;
                font-weight: normal;
                text-transform:capitalize;
            }
            .EU_DataTable tr:nth-child(2n+2)
            {
                background-color: #f3f4f5;
            }

            .EU_DataTable tr:nth-child(2n+1) td
            {
                background-color: #d6dadf;
                color: #454545;
            }
            .EU_DataTable tr td
            {
                padding: 5px 10px 5px 10px;
                color: #454545;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 11px;
                border: 1px solid #cccccc;
                vertical-align: middle;
            }
                .EU_DataTable tr td:first-child
                {
                    text-align: center;
                }
        /*-----------------------------------------------------------------*/
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
        function PrintPanels() {
            var panel = document.getElementById("<%=pnlpopup.ClientID %>");
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
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..."
                    ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 38%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <div class="legagentsms">
                <fieldset class="fontt">
                    <legend style="color: #3399FF">Agent Details </legend>
                    <table border="0" width="100%" id="table1" cellspacing="1" align="center">
                        <tr>
                            <td width="10%">
                                &nbsp;</td>
                            <td align="right" width="39%" height="30px">
                                <asp:Label ID="Label3" runat="server" Text="Plant Name"></asp:Label>
                            </td>
                            <td width="2%">
                            </td>
                            <td align="left" width="39%">
                                <asp:DropDownList ID="ddl_Plantname" CssClass="DText"  runat="server" AutoPostBack="true" 
                                    Font-Bold="True" Font-Size="small" 
                                    onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                            <td width="10%">
                            </td>
                        </tr>
                        <tr>
                         <td width="10%">
                             <asp:DropDownList ID="ddl_RouteID"  runat="server" AutoPostBack="True" 
                                 Height="18px"  
                                 Visible="false" Width="20px" 
                                 onselectedindexchanged="ddl_RouteID_SelectedIndexChanged">
                             </asp:DropDownList>
                            </td>
                             <td width="39%" align="right" height="30px">
                                 <asp:Label ID="lbl_RouteName"  runat="server" Text="Route Name"></asp:Label>
                            </td>
                             <td width="2%">
                            </td>
                             <td width="39%">
                                 <asp:DropDownList ID="ddl_RouteName" CssClass="DText" runat="server" AutoPostBack="True" 
                                     Font-Bold="True" Font-Size="small" 
                                     onselectedindexchanged="ddl_RouteName_SelectedIndexChanged" >
                                     
                                 </asp:DropDownList>
                            </td>
                             <td width="10%">
                            </td>
                        </tr>
                        <tr>
                         <td width="10%">
                             &nbsp;</td>
                             <td width="39%" align="right" height="30px">
                                 <asp:Label ID="lbl_AgentName" runat="server" Text="Agent Name"></asp:Label>
                            </td>
                             <td width="2%">
                            </td>
                             <td width="39%">
                                 <asp:DropDownList ID="ddl_AgentName" CssClass="DText" runat="server" 
                                     Font-Bold="True" Font-Size="small"   >
                                  
                                 </asp:DropDownList>
                                 <asp:Button ID="btnPrintCurrent" runat="server" BackColor="#00CCFF" 
                                     BorderStyle="Double" Font-Bold="True" ForeColor="White" 
                                     OnClientClick="return PrintPanel();" Text="Print" />
                            </td>
                             <td width="10%">
                            </td>
                        </tr>
                        <tr>
                         <td width="10%">
                            </td>
                             <td width="39%">
                            </td>
                             <td width="2%">
                            </td>
                             <td width="39%">
                                 &nbsp;</td>
                             <td width="10%">
                            </td>
                        </tr>
                         <tr align="center">
                         <td colspan="5">
                             <asp:GridView ID="gvCustomers" runat="server"  
                                 AutoGenerateColumns="false" AutoGenerateSelectButton="false" CssClass="fontfam" 
                                 Font-Size="12px" HeaderStyle-BackColor="Silver" 
                                 OnSelectedIndexChanging="gvCustomers_SelectedIndexChanged" PageSize="4">
                                 <Columns>
                                     <asp:BoundField DataField="Sno1" HeaderText="Sno" />
                                     <asp:BoundField DataField="ReportType" HeaderText="ReportType" />
                                     <asp:CommandField ButtonType="Button" SelectText="Display" ShowSelectButton="True" />
                                 </Columns>
                             </asp:GridView>
                         </td>
                        </tr>
                         <tr align="center">
                         <td colspan="5">
                  
                           </td>
                        </tr>
                    </table>
                    <br />
                </fieldset>
            </div>
            <table border="0" width="100%" id="tab" cellspacing="1" align="center" captionalign="Top">
                <tr>
                    <td width="100%">
                        <table border="0" width="100%" id="table4" cellspacing="1" align="center">
                            <tr>
                                <td>
                                </td>
                                <td align="left">
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                                <td align="left">
                                    &nbsp;
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
                    <table width="100%" border="1" >
                        <tr>
                            <td colspan="4" align="center" width="100%">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/VLogo.png" Width="70px" Height="70px" />
                                <asp:Label ID="Label1" runat="server" Text="SRI VYSHNAVI DAIRY SPECIALITIES (P) LIMITED"
                                    Font-Bold="true" Font-Size="16px"></asp:Label>
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Label ID="Lbt_Pname" runat="server"  Font-Bold="true" Font-Size="14px"></asp:Label>
                                <br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Label ID="Lbt_Title" runat="server" Text="AgentDetails Report" Font-Bold="true" Font-Size="14px"></asp:Label>
                                <%-- &nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="From" Font-Bold="true"
                        Font-Size="11px"></asp:Label><asp:Label ID="Label5" runat="server" Font-Bold="true"
                            Font-Size="11px"></asp:Label><asp:Label ID="Label6" runat="server" Text="To" Font-Bold="true"
                                Font-Size="11px"></asp:Label><asp:Label ID="Label7" runat="server" Font-Bold="true"
                                    Font-Size="11px"></asp:Label>--%>
                                
                            </td>
                        </tr>
                        <div></div>
                        <tr valign="top" >
                            
                            <td align="right" width="5%">
                             <div class="EU_TableScroll" id="showData" style="display: block">
                                <asp:GridView ID="gvresult" runat="server"  AutoGenerateColumns="true"
                                    CssClass="EU_DataTable" Font-Size="12px" HeaderStyle-BackColor="Silver" 
                                    OnRowDataBound="gvresult_RowDataBound" >
                                </asp:GridView>
                                </div>
                            </td>
                            <td align="left" width="5%">
                            <div class="EU_TableScroll" id="Div1" style="display: block">
                                <asp:GridView ID="gvmilkresult" runat="server"  AutoGenerateColumns="true"
                                    CssClass="EU_DataTable" Font-Size="12px" HeaderStyle-BackColor="Silver"  >
                                </asp:GridView>
                                </div>
                            </td>
                            <td align="left" width="5%">
                             <div class="EU_TableScroll" id="Div2" style="display: block">
                                <asp:GridView ID="gvLoanresult" runat="server"  AutoGenerateColumns="true"
                                    CssClass="EU_DataTable" Font-Size="12px" HeaderStyle-BackColor="Silver" 
                                     onrowdatabound="gvLoanresult_RowDataBound" 
                                    OnSelectedIndexChanging="gvLoanresult_SelectedIndexChanged" >
                                    <Columns>
                                     <asp:CommandField ButtonType="Button" SelectText="Details" ShowSelectButton="True" />
                                    </Columns>
                                </asp:GridView>
                                </div>
                            </td>
                        </tr>
                           <tr valign="top" >
                            <td align="right" width="5%">
                            </td>
                            <td align="left" width="5%">
                            <div class="EU_TableScroll" id="Div3" style="display: block">
                             <asp:GridView ID="gvInventory" runat="server"  AutoGenerateColumns="true"
                                    CssClass="EU_DataTable" Font-Size="12px" HeaderStyle-BackColor="Silver" 
                                    PageSize="12"  >
                                </asp:GridView>
                                 </div>
                            </td>
                            <td align="left" width="5%">
                            </td>
                            </tr>
                              <tr valign="top" >
                            <td align="right" width="5%">
                            </td>
                            <td align="left" width="5%">
                            <div class="EU_TableScroll" id="Div4" style="display: block">
                             <asp:GridView ID="gvSupervisor" runat="server"  AutoGenerateColumns="true"
                                     CssClass="EU_DataTable" Font-Size="12px" HeaderStyle-BackColor="Silver" 
                                    PageSize="12"  >
                                </asp:GridView>
                                </div>
                            </td>
                            <td align="left" width="5%">
                            </td>
                            </tr>
                    </table>
                </div>
            </asp:Panel>
            <div>
           
             <asp:Button ID="btn_modsample" runat="server" style="display:none" />
              <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btn_modsample"  PopupControlID="pnlpopup" 
 BackgroundCssClass="modalBackground"> </asp:ModalPopupExtender>
  <div class="perdaymilkstatus"> 
  <asp:Panel ID="pnlpopup" runat="server" CssClass="modalPopup" style="display:inline">
  <div align="right">
    <asp:Button ID="Button1" runat="server" BackColor="#00CCFF" BorderStyle="Double" Font-Bold="True" ForeColor="White" OnClientClick="return PrintPanels();" Text="Print" Visible="false" />    
    <asp:Button ID="btn_Mclose" runat="server" BackColor="#00CCFF" BorderStyle="Double" Font-Size="10"  ForeColor="White" Text="X" onclick="btn_Mclose_Click" />     
  </div>
  <table>
  <tr>
  <td align="center" > 
    <asp:Label ID="Lbl_NameT" runat="server" ForeColor="Red" Text="Name :"></asp:Label>
  <asp:Label ID="Lbl_NameR" runat="server" ForeColor="Red"></asp:Label>
  <br />
  <br />
     <asp:Label ID="Lbl_LdateT" runat="server" ForeColor="Red" Text="LoanDate :"></asp:Label>
  <asp:Label ID="Lbl_LdateR" runat="server" ForeColor="Red"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;
   <asp:Label ID="Lbl_LCodeT" runat="server" ForeColor="Red" Text="LoanId :"></asp:Label>
  <asp:Label ID="Lbl_LCodeR" runat="server" ForeColor="Red"></asp:Label>
    <div class="EU_TableScroll" id="Div5"   style="display: block" >   
   <asp:GridView ID="gvAgentloanRecoverydetails" runat="server"  AutoGenerateColumns="true"
                                    CssClass="EU_DataTable" Font-Size="12px" HeaderStyle-BackColor="Silver" 
                                    PageSize="12"  >
                                </asp:GridView>
                                </div> 
  </td>
  </tr>
  </table>
  </asp:Panel> 
  </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
