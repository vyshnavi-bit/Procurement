<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DpuBankPaymentSelectedList.aspx.cs" Inherits="DpuBankPaymentSelectedList" %>
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
      
  <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
             
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%"><br />
                <table class="style1">
                    <tr align="right">
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" 
                                PostBackUrl="~/DpuBankPaymentAllotment.aspx">Bank Payment Allotment</asp:LinkButton>
                        </td>
                    </tr>
                </table>
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
                      &nbsp;</td>
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
   
<div class="legendloan">
<fieldset class="fontt">
<legend class="fontt">SelectedList Report</legend>
<table width="100%">
<tr>
  <td  class="style1"></td>
         <td align="right">
            
             <asp:Label ID="lbl_Plantcode" runat="server" Text="Plant ID" Visible="False"></asp:Label>
            
         </td>
         <td align="right" >         
   
             <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" 
                    Visible="false" Font-Bold="True" Font-Size="14px" Width="190px" 
                 onselectedindexchanged="ddl_Plantcode_SelectedIndexChanged" 
                 CssClass="tb10"></asp:DropDownList>
   
         </td>
         </tr>
<tr>
  <td  class="style1"></td>
         <td align="right">
         <asp:Label ID="lbl_PlantName" runat="server" Text="Plant Name"></asp:Label>

         </td>
         <td align="right" >
                   <asp:DropDownList ID="ddl_Plantname" AutoPostBack="true" runat="server" 
                    Width="190px" Font-Bold="True" Font-Size="12px" 
                       onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                       CssClass="tb10">
                                    </asp:DropDownList>
         </td>
         </tr>
         <tr>
  <td class="style1"></td>
         <td align="right">
   
             <asp:CheckBox ID="chk_All" runat="server" AutoPostBack="True" 
                 Checked="True"  Text="ALL" oncheckedchanged="chk_All_CheckedChanged" />
   
           <asp:Label ID="lbl_addeddate" runat="server" Text="   /Date"></asp:Label>
             &nbsp;</td>
         <td align="right">
   
             <asp:DropDownList ID="ddl_Addeddate" AutoPostBack="true" runat="server" 
                    Visible="true" Font-Bold="True" Font-Size="14px" Width="190px" 
                 CssClass="tb10"></asp:DropDownList>
   
         </td>
         </tr>
         <tr>
             <td class="style1">
                 &nbsp;</td>
             <td align="right">
                 <asp:Label ID="lblplant" runat="server" Text="Plant Type"></asp:Label>
             </td>
             <td align="center">
                 <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                     RepeatDirection="Horizontal" style="margin-left: 0px">
                     <asp:ListItem Value="BUFF">BUFF</asp:ListItem>
                     <asp:ListItem Value="COW">COW</asp:ListItem>
                 </asp:RadioButtonList>
             </td>
    </tr>
         <tr> 
<td class="style1"></td>
         <td  align="center" colspan="2">         
             <asp:Label ID="Lbl_selectedReportItem" runat="server" visible="false"></asp:Label>
              <asp:RadioButtonList ID="rbtLstReportItems"   RepeatDirection="Horizontal" 
                 RepeatLayout="Table"  runat="server" CssClass="tb10">
                 <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                  <asp:ListItem Text="WSbi" Value="WSbi"></asp:ListItem>
                  <asp:ListItem Text="PSbi" Value="PSbi"></asp:ListItem>
                  <asp:ListItem Text="Ing" Value="Ing"></asp:ListItem>
                  <asp:ListItem Text="Ingoth" Value="Ing1"></asp:ListItem>
                  <asp:ListItem Text="Hdfc" Value="Hdfc"></asp:ListItem>
                  <asp:ListItem Text="Hdfcoth" Value="Hdfcoth"></asp:ListItem>
                  <asp:ListItem Text="pendingList" Value="pendingList"></asp:ListItem>                  
                </asp:RadioButtonList>
             </td>
 
               
 

</tr>

<tr> 
<td class="style1"></td>
         <td  align="right">
         
             &nbsp;</td>
 <td  align="right" >

 <asp:Button ID="btn_Submit" runat="server"  
         BackColor="#6F696F"  ForeColor="White"
            Text="Generate" Height="26px" onclick="btn_Submit_Click" 
         CssClass="tb10"/>
  &nbsp;<asp:Button ID="btn_ok" runat="server"  
         BackColor="#6F696F"  ForeColor="White"
            Text="ALL" Width="50px" Height="26px" onclick="btn_ok_Click" />
        &nbsp;<asp:Button ID="btn_Export" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="Export" Width="60px" Height="26px" onclick="btn_Export_Click"  />
            <br />
 </td>

</tr>
<tr> 
<td class="style1"></td>
         <td  align="right">
         
             &nbsp;</td>
 <td  align="right" >

            <asp:Button ID="btn_Exportcsv" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="SBI.CSV" Height="26px" onclick="btn_Exportcsv_Click" 
          />

            <asp:Button ID="btn_SbipaymentListcsv0" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="SBI .CSV1" Height="26px" 
                onclick="btn_SbipaymentListcsv0_Click" 
          />
 </td>

</tr>
<tr> 
<td class="style1"></td>
         <td  align="right">
         
             &nbsp;</td>
 <td  align="right" >

        &nbsp;<asp:Button ID="btn_ExportIng" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="Export ING" Height="26px" onclick="btn_ExportIng_Click"  />
            <asp:Button ID="btn_ExportIngcsv" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="Export ING1" Height="26px" onclick="btn_ExportIngcsv_Click" 
                 />
        <asp:Button ID="btn_Hdfc" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="Hdfc" Width="60px" Height="26px" onclick="btn_Hdfc_Click"/>
        <br />
 </td>

</tr>

</table>
</fieldset>


</div>
  
   <center>
   <table align="center" width="400px">
            <tr>
                <td colspan="2" style="background-image: url(Image/header.jpg); height: 10px">                   
                </td>
            </tr>
            <tr>
            <td>
             <asp:GridView ID="GridView7" runat="server" 
                    onrowdatabound="GridView7_RowDataBound" 
                    onselectedindexchanged="GridView7_SelectedIndexChanged" >

             <Columns>
                     <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                 </Columns>


             </asp:GridView>
            </td>
            </tr>
             <tr>
                <td colspan="2" style="background-image: url(Image/header.jpg); height: 10px">

                   <asp:GridView ID="GridView8" runat="server"  ShowFooter="true"
                        CssClass="table table-striped table-bordered table-hover" 
                        onrowdatabound="GridView8_RowDataBound">
                         <HeaderStyle ForeColor="#660066" HorizontalAlign="Right"  />
                           <FooterStyle ForeColor="#660066"  HorizontalAlign="Right" />
                           <Columns>
									   <asp:TemplateField HeaderText="SNo.">
										   <ItemTemplate>
											   <%# Container.DataItemIndex + 1 %>
										   </ItemTemplate>
									   </asp:TemplateField>
								   </Columns>
                    </asp:GridView>


                </td>
            </tr>
        </table>
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
            <tr>
                <td align="center"> 
                <asp:GridView ID = "GridView4" runat = "server" AutoGenerateColumns = "false" 
                        OnDataBound = "OnDataBound" OnRowCreated = "OnRowCreated"  ForeColor="Black" 
                        GridLines="Vertical"  BackColor="White" BorderColor="Black" 
                        BorderStyle="Solid" BorderWidth="1px" CssClass="serh-grid" 
                        onselectedindexchanged="GridView4_SelectedIndexChanged" >
<Columns>
    <asp:BoundField DataField = "Agent_Id" HeaderText = "Agent ID" ItemStyle-Width = "80" ItemStyle-BorderStyle="Solid" />
    <asp:BoundField DataField = "ProductName" HeaderText = "Agent Name" ItemStyle-Width = "200" ItemStyle-BorderStyle="Solid" />
    <asp:BoundField DataField = "Price" HeaderText = "Amount" ItemStyle-Width = "100" DataFormatString = "{0:N2}" ItemStyle-HorizontalAlign = "Right" ItemStyle-BorderStyle="Solid" />
     <asp:BoundField DataField = "Bankname" HeaderText = "BankName" ItemStyle-Width = "200" ItemStyle-BorderStyle="Solid" />
</Columns>
                       <FooterStyle BackColor="#CCCC99" />
                        <SelectedRowStyle CssClass="grid-sltrow" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" BorderStyle="Solid"
                            BorderWidth="1px" BorderColor="Black" />
</asp:GridView>
</td>
               
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
            </Triggers>
   </asp:UpdatePanel>  

        </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

