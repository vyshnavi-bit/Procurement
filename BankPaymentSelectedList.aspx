<%@ Page Title="OnlineMilkTest|SelectBanPaymentList" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BankPaymentSelectedList.aspx.cs" Inherits="BankPaymentSelectedList" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

    .disnone
    {
        display:none;
    }
        .serh-grid
        {
            width: 85%;
            border: 1px solid #6AB5FF;
            background: #fff;
            line-height: 14px;
            font-size: 11px;
            font-family: Verdana;
        }
        
         .grid-sltrow
        {
            background: #ddd;
            font-weight: bold;
        }
        .SubTotalRowStyle
        {
            border: solid 1px Black;
            background-color: #D8D8D8;
            font-weight: bold;
        }
        .GrandTotalRowStyle
        {
            border: solid 1px Gray;
            background-color: #000000;
            color: #ffffff;
            font-weight: bold;
        }
        .GroupHeaderStyle
        {
            border: solid 1px Black;
            background-color: #4682B4;
            color: #ffffff;
            font-weight: bold;
        }
        .serh-grid
        {
            width: 85%;
            border: 1px solid #6AB5FF;
            background: #fff;
            line-height: 14px;
            font-size: 11px;
            font-family: Verdana;
        }
        
        .style1
        {
            width: 100%;
        }
         .style112
        {
            width: 60%;
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

<script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
<script type="text/javascript">
    $(function () {
        blinkeffect('#lbl_totamt');
    })
    function blinkeffect(selector) {
        $(selector).fadeOut('slow', function () {
            $(this).fadeIn('slow', function () {
                blinkeffect(this);
            });
        });
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
      
  <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
             
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;BankPayment SelectedList</p>
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
                
            </td>
        </tr>
        </table>

 
<div style="width:100%;">
                  <table   width="100%">
                  <tr>
                  <td width="18%">
                      <asp:HyperLink ID="HyperLink1" runat="server"  CssClass="fontt"
                          NavigateUrl="~/BankPaymentAllotment.aspx">Go to Allot Page</asp:HyperLink>
                      </td>
                   <td width="10%" class="fontt">
										From Date:</td>
										
                    <td width="15%" class="fontt" align="right">
                                <asp:TextBox ID="txt_FromDate" runat="server" Enabled="False"  ></asp:TextBox>

                            </td>
                                <td width="10%">
                                 
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                              
                               </td>
                       <td width="9%" class="fontt">
       
                         To Date:</td>
                             
                               <td width="15%" class="fontt" align="right">
                              <asp:TextBox ID="txt_ToDate" runat="server" Enabled="False"  ></asp:TextBox></td>
                               <td width="12%">
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                         
                          </td> 
                          <td width="20%"></td>    
                       </tr>
                   </table>
   </div>
   <br />
   
   <center>
<div  class=style112 align=center>
<fieldset class="fontt">
<legend class="fontt">SelectedList Report</legend>
<table width="100%">
<tr>
         <td align="right" style="text-align: center" colspan="2">
            
             <asp:Label ID="lbl_Plantcode" runat="server" Text="Plant ID" Visible="False"></asp:Label>
            
             <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                 Font-Bold="True" Font-Size="Large" 
                 onselectedindexchanged="ddl_Plantcode_SelectedIndexChanged" Visible="false" 
                 Width="190px">
             </asp:DropDownList>
            
         </td>
         </tr>
<tr>
         <td align="right" style="text-align: center" colspan="2">
         <asp:Label ID="lbl_PlantName" runat="server" Text="Plant Name"></asp:Label>

             <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
                 Font-Bold="True" Font-Size="Large" 
                 onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="190px">
             </asp:DropDownList>

         </td>
         </tr>
         <tr>
         <td align="right" style="text-align: center" colspan="2">
   
             <asp:CheckBox ID="chk_All" runat="server" AutoPostBack="True" 
                 Checked="True"  Text="ALL" oncheckedchanged="chk_All_CheckedChanged" />
   
           <asp:Label ID="lbl_addeddate" runat="server" Text="   /Date"></asp:Label>
             &nbsp;<asp:DropDownList ID="ddl_Addeddate" runat="server" AutoPostBack="true" 
                 Font-Bold="True" Font-Size="Large" Visible="true" Width="190px" 
                 onselectedindexchanged="ddl_Addeddate_SelectedIndexChanged" 
                 ontextchanged="ddl_Addeddate_TextChanged">
             </asp:DropDownList>
             </td>
         </tr>
         <tr align=center>
             <td align=center WIDTH=50% style="text-align: right"> 
                 <asp:Label ID="lblplant" runat="server" Text="Plant Type"></asp:Label>
             </td>
             <td align="LEFT"  WIDTH=50%> 
                 <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                     RepeatDirection="Horizontal" style="margin-left: 0px">
                     <asp:ListItem Value="BUFF">BUFF</asp:ListItem>
                     <asp:ListItem Value="COW">COW</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
    </tr>
         <tr> 
         <td  align="center" colspan="2">         
             <asp:Label ID="Lbl_selectedReportItem" runat="server" visible="false"></asp:Label>
              <asp:RadioButtonList ID="rbtLstReportItems"   RepeatDirection="Horizontal" 
                 RepeatLayout="Table"  runat="server" AutoPostBack="True" 
                 onselectedindexchanged="rbtLstReportItems_SelectedIndexChanged">
                 <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                  <asp:ListItem Text="WSbi" Value="WSbi"></asp:ListItem>
                  <asp:ListItem Text="PSbi" Value="PSbi"></asp:ListItem>
                  <asp:ListItem Text="Ing" Value="Ing"></asp:ListItem>
                  <asp:ListItem Text="Ingoth" Value="Ing1"></asp:ListItem>
                  <asp:ListItem Text="Hdfc" Value="Hdfc"></asp:ListItem>
                  <asp:ListItem Text="Hdfcoth" Value="Hdfcoth"></asp:ListItem>
                  <asp:ListItem Text="pendingList" Value="pendingList"></asp:ListItem>                  
                  <asp:ListItem Value="8">Kotack Mahindra</asp:ListItem>
                </asp:RadioButtonList>
             </td>
 
               
 

</tr>

    <tr>
        <td align="right" style="text-align: center" colspan="2">
            <asp:Button ID="btn_Submit" runat="server" BackColor="#6F696F" 
                ForeColor="White" Height="26px" onclick="btn_Submit_Click" Text="Generate" />
            <asp:Button ID="btn_ok" runat="server" BackColor="#6F696F" ForeColor="White" 
                Height="26px" onclick="btn_ok_Click" Text="ALL" Width="50px" />
            <asp:Button ID="btn_Export" runat="server" BackColor="#6F696F" 
                ForeColor="White" Height="26px" onclick="btn_Export_Click" Text="Export" 
                Width="60px" />
        </td>
    </tr>
    <tr>
        <td align="right" style="text-align: center" colspan="2">
            <asp:Button ID="btn_Exportcsv" runat="server" BackColor="#6F696F" 
                ForeColor="White" Height="26px" onclick="btn_Exportcsv_Click" Text="SBI.CSV" />
            <asp:Button ID="btn_SbipaymentListcsv0" runat="server" BackColor="#6F696F" 
                ForeColor="White" Height="26px" onclick="btn_SbipaymentListcsv0_Click" 
                Text="SBI .CSV1" />
        </td>
    </tr>
    <tr>
        <td align="right" style="text-align: center" colspan="2">
            <asp:Button ID="btn_ExportIng" runat="server" BackColor="#6F696F" 
                ForeColor="White" Height="26px" onclick="btn_ExportIng_Click" 
                Text="Export ING" />
            <asp:Button ID="btn_ExportIngcsv" runat="server" BackColor="#6F696F" 
                ForeColor="White" Height="26px" onclick="btn_ExportIngcsv_Click" 
                Text="Export ING1" />
            <asp:Button ID="btn_Hdfc" runat="server" BackColor="#6F696F" ForeColor="White" 
                Height="26px" onclick="btn_Hdfc_Click" Text="Hdfc" Width="60px" />
        </td>
    </tr>
<tr> 
         <td  align="right" style="text-align: center" colspan="2">
         
             <asp:Label ID="lbl_ktk" runat="server" Text="Bank A/c"></asp:Label>
         
             &nbsp;<asp:DropDownList ID="ddl_kotack" runat="server" AutoPostBack="true" 
                 Font-Bold="True" Font-Size="Large" Visible="true" Width="190px">
                 <asp:ListItem Value="425044000438">425044000438</asp:ListItem>
                 <asp:ListItem>328044039913</asp:ListItem>
                 <asp:ListItem>334044049195</asp:ListItem>
                 <asp:ListItem>337044040029</asp:ListItem>
                 <asp:ListItem>334044032411</asp:ListItem>
             </asp:DropDownList>
             <asp:Button ID="btn_kotack" runat="server" BackColor="#6F696F" 
                 ForeColor="White" Height="26px" onclick="btn_kotack_Click" 
                 style="font-weight: 700" Text="Kotack" Width="60px" />
             <asp:Button ID="btn_kotackexport" runat="server" BackColor="#6F696F" 
                 ForeColor="White" Height="26px" onclick="btn_kotackexport_Click" 
                 style="font-weight: 700" Text="KOTACK EXPORT" Width="150px" />
             <br />
         </td>

</tr>

    <tr>
        <td align="right" colspan="2" style="text-align: center">
            <span>
            <div id="txtblnk"> <p>&nbsp;</p> </div>
            </span>
        </td>
    </tr>

</table>
</fieldset>
</center>

</div>
  
   <center>
   <table align="center" class="text2" width="400px">
            <tr valign=top align="center">
                <td colspan="2" style="background-image: url(Image/header.jpg); height: 10px">                   
                    <span>
                 
                    </span></td>
                  <form>
<div id="Div1"> </div>
            </tr>
            <caption>
                <p>
                    <asp:Label ID="lbl_totamt" runat="server" Text="Label" 
                        style="color: #000066; font-weight: 700"></asp:Label>
                </p>
                </div>
                </form>
                </tr>
                <tr align="center">
                    <td valign="top">
                        <asp:GridView ID="GridView7" runat="server" 
                            onrowdatabound="GridView7_RowDataBound" 
                            onselectedindexchanged="GridView7_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2" style="background-image: url(Image/header.jpg); height: 10px">
                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" 
                            BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                            CssClass="serh-grid" ForeColor="Black" GridLines="Vertical" 
                            OnDataBound="OnDataBound" OnRowCreated="OnRowCreated" 
                            onselectedindexchanged="GridView4_SelectedIndexChanged" Visible="False">
                            <Columns>
                                <asp:BoundField DataField="Agent_Id" HeaderText="Agent ID" 
                                    ItemStyle-BorderStyle="Solid" ItemStyle-Width="80" />
                                <asp:BoundField DataField="ProductName" HeaderText="Agent Name" 
                                    ItemStyle-BorderStyle="Solid" ItemStyle-Width="200" />
                                <asp:BoundField DataField="Price" DataFormatString="{0:N2}" HeaderText="Amount" 
                                    ItemStyle-BorderStyle="Solid" ItemStyle-HorizontalAlign="Right" 
                                    ItemStyle-Width="100" />
                                <asp:BoundField DataField="Bankname" HeaderText="BankName" 
                                    ItemStyle-BorderStyle="Solid" ItemStyle-Width="200" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <SelectedRowStyle CssClass="grid-sltrow" />
                            <HeaderStyle BackColor="#6B696B" BorderColor="Black" BorderStyle="Solid" 
                                BorderWidth="1px" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                        <br />
                    </td>
                </tr>
            </caption>
        </table>
           <table class="style1">
               <tr>
                   <td>
                       <asp:GridView ID="GridView9" runat="server" CssClass="serh-grid" Width="200%">
                           <HeaderStyle ForeColor="#660066" HorizontalAlign="Right" />
                           <FooterStyle ForeColor="#660066" HorizontalAlign="Right" />
                       </asp:GridView>
                   </td>
               </tr>
               <tr>
                   <td>
                       <asp:GridView ID="GridView8" runat="server" CssClass="serh-grid" 
                           onrowdatabound="GridView8_RowDataBound" ShowFooter="true" Width="90%">
                           <HeaderStyle ForeColor="#660066" HorizontalAlign="Right" />
                           <FooterStyle ForeColor="#660066" HorizontalAlign="Right" />
                           <Columns>
                               <asp:TemplateField HeaderText="SNo.">
                                   <ItemTemplate>
                                       <%# Container.DataItemIndex + 1 %>
                                   </ItemTemplate>
                               </asp:TemplateField>
                           </Columns>
                       </asp:GridView>
                       <asp:Button ID="Button2" runat="server" CssClass="button" Font-Size="Smaller" 
                           onclick="Button2_Click" Text="Export" />
                   </td>
               </tr>
       </table>
       <br />
           <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="X-Small"        
         PageSize="2" EnableViewState="False"            
            CssClass="gridview1" Font-Italic="False">
            <Columns>
             <asp:BoundField DataField="Agent_Name" HeaderText="Name" 
                SortExpression="Agent_Name" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="Account_no" HeaderText="A/C No" 
                SortExpression="Account_no" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="Standard" HeaderText="Standard" 
                SortExpression="Standard" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#99CCCC" Font-Size="Small" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>

           <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="X-Small"        
         PageSize="2" EnableViewState="False"            
            CssClass="gridview1" Font-Italic="False">
            <Columns>
             <asp:BoundField DataField="Account_no" HeaderText="Ac_no" 
                SortExpression="Account_no" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="NetAmount" HeaderText="NetAmount" 
                SortExpression="NetAmount" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="Adate" HeaderText="Adate" 
                SortExpression="Adate" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="NetAmount" HeaderText="NetAmount1" 
                SortExpression="NetAmount" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="agent_id" HeaderText="Agent_id" 
                SortExpression="agent_id" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
              <asp:BoundField DataField="Standards" HeaderText="Standards" 
                SortExpression="Standards" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#99CCCC" Font-Size="Small" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>

           <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="X-Small"        
         PageSize="2" EnableViewState="False"            
            CssClass="gridview1" Font-Italic="False">
            <Columns>
             <asp:BoundField DataField="BeneficiaryName" HeaderText="BeneficiaryName" 
                SortExpression="BeneficiaryName" ReadOnly="True">
            <ControlStyle Width="60px" />
            <FooterStyle Width="60px" />
            <HeaderStyle Width="60px" />
            <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="BeneficiaryBankName" HeaderText="BeneficiaryBankName" 
                SortExpression="BeneficiaryBankName" ReadOnly="True">
            <ControlStyle Width="60px" />
            <FooterStyle Width="60px" />
            <HeaderStyle Width="60px" />
            <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="AccountNo" HeaderText="AccountNo" 
                SortExpression="AccountNo" ReadOnly="True">
            <ControlStyle Width="60px" />
            <FooterStyle Width="60px" />
            <HeaderStyle Width="60px" />
            <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="BeneficiaryAccountType" HeaderText="BeneficiaryAccountType" 
                SortExpression="BeneficiaryAccountType" ReadOnly="True">
            <ControlStyle Width="60px" />
            <FooterStyle Width="60px" />
            <HeaderStyle Width="60px" />
            <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="IFSCCode" HeaderText="IFSCCode" 
                SortExpression="IFSCCode" ReadOnly="True">
            <ControlStyle Width="60px" />
            <FooterStyle Width="60px" />
            <HeaderStyle Width="60px" />
            <ItemStyle Width="60px" />
            </asp:BoundField>
              <asp:BoundField DataField="Amount" HeaderText="Amount" 
                SortExpression="Amount" ReadOnly="True">
            <ControlStyle Width="60px" />
            <FooterStyle Width="60px" />
            <HeaderStyle Width="60px" />
            <ItemStyle Width="60px" />
            </asp:BoundField>
             <asp:BoundField DataField="SendertoReceiverInfo" HeaderText="SendertoReceiverInfo" 
                SortExpression="SendertoReceiverInfo" ReadOnly="True">
            <ControlStyle Width="60px" />
            <FooterStyle Width="60px" />
            <HeaderStyle Width="60px" />
            <ItemStyle Width="60px" />
            </asp:BoundField>
             <asp:BoundField DataField="OwnReferenceNumber" HeaderText="OwnReferenceNumber" 
                SortExpression="OwnReferenceNumber" ReadOnly="True">
            <ControlStyle Width="60px" />
            <FooterStyle Width="60px" />
            <HeaderStyle Width="60px" />
            <ItemStyle Width="60px" />
            </asp:BoundField>
             <asp:BoundField DataField="Remarks" HeaderText="Remarks" 
                SortExpression="Remarks" ReadOnly="True">
            <ControlStyle Width="60px" />
            <FooterStyle Width="60px" />
            <HeaderStyle Width="60px" />
            <ItemStyle Width="60px" />
            </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#99CCCC" Font-Size="Small" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>

            <br />
             <asp:GridView ID="GridView5" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="X-Small"        
         PageSize="2" EnableViewState="False"            
            CssClass="gridview1" Font-Italic="False">
            <Columns>
             <asp:BoundField DataField="ACCOUNT" HeaderText="ACCOUNT" 
                SortExpression="ACCOUNT" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="C" HeaderText="C" 
                SortExpression="C" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" 
                SortExpression="AMOUNT" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
             <asp:BoundField DataField="NARRATION" HeaderText="NARRATION" 
                SortExpression="NARRATION" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#99CCCC" Font-Size="Small" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>

             <br />
             <asp:GridView ID="GridView6" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="X-Small"        
         PageSize="2" EnableViewState="False"            
            CssClass="gridview1" Font-Italic="False">
            <Columns>
             <asp:BoundField DataField="TranType" HeaderText="TranType" 
                SortExpression="TranType" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
             <asp:BoundField DataField="ACCOUNT" HeaderText="ACCOUNT" 
                SortExpression="ACCOUNT" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
             <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" 
                SortExpression="AMOUNT" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="AgentName" HeaderText="AgentName" 
                SortExpression="AgentName" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="Agent_Id" HeaderText="Agent_Id" 
                SortExpression="Agent_Id" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
             <asp:BoundField DataField="PayDate" HeaderText="PayDate" 
                SortExpression="PayDate" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
             <asp:BoundField DataField="Ifsccode" HeaderText="Ifsccode" 
                SortExpression="Ifsccode" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
             <asp:BoundField DataField="BankName" HeaderText="BankName" 
                SortExpression="BankName" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
             <asp:BoundField DataField="Pmail" HeaderText="Pmail" 
                SortExpression="Pmail" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#99CCCC" Font-Size="Small" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>

            </center>




     <div  class="disnone">
        <table cellpadding="0" cellspacing="5" class="tableInfo">
            <tr valign=top>
                <td valign=top align="center"> 
                    &nbsp;</td>
               
            </tr>
        </table>        
        </div>




<br />


  <table>
       <tr>
    <td width="15%">&nbsp;</td>
            <td width="16%" align="left">
        
                &nbsp;</td>
            </tr>
       </table>
       
      </ContentTemplate>
         <Triggers>
                <asp:PostBackTrigger  ControlID="btn_Submit"  />
                 <asp:PostBackTrigger  ControlID="btn_ok"  />
                  <asp:PostBackTrigger  ControlID="btn_Export"  />
                   <asp:PostBackTrigger  ControlID="btn_Exportcsv"  />
                     <asp:PostBackTrigger  ControlID="btn_SbipaymentListcsv0"  />
                       <asp:PostBackTrigger  ControlID="btn_ExportIng" />
                         <asp:PostBackTrigger  ControlID="btn_ExportIngcsv"   />
                            <asp:PostBackTrigger  ControlID="btn_Hdfc"   />
                               <asp:PostBackTrigger  ControlID="Button2"   />
                                 <asp:PostBackTrigger  ControlID="btn_kotackexport"   />
            </Triggers>
   </asp:UpdatePanel>  

        </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

