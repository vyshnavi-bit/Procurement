<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OverHeadEntry.aspx.cs" Inherits="OverHeadEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <head>

<style type="text/css">

.style1
 {
  height: 20px;
 }
    .style5
        {
            font-family: Andalus;
        }
 .modalPopup
{
background-color: #FFFFFF;
filter: alpha(opacity=40);
opacity: 0.7;
xindex:-1;
}
    .style2
    {
        height: 25px;
    }
    .style3
    {
        font-family: Andalus;
        font-size: medium;
    }
    .style5
    {
        font-size: medium;
        font-family: Andalus;
        color: #333300;
    }
    #table4
    {
        width: 409px;
    }
    #Cheque
    {
        width: 64%;
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

</head>
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server" />
 <div>
 <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height: 100%; width: 10%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>   
 <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
    <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%">
                <div align="right">
                    <asp:HyperLink ID="HyperLink3" runat="server" CssClass="fontt" 
                        NavigateUrl="~/Accounts_TransactionReport.aspx">Accounts Transaction Report</asp:HyperLink>
                </div>
                </td>
        </tr>
    </table>
    <center>
 <%-- // <div class="legagentsms">--%>
  <center>
   <fieldset style="background-color: #CCFFFF; width: 421px;>
   <legend class="fontt"><span class="style3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>OverHeads Entry</strong></span><br /> </legend>
   &nbsp;<center>
         <table border="0" id="table4" cellspacing="1" align="center" style="tb10">       
         <tr>
                   <td width="5%" class="style1">
                   </td>
                   <td width="30%" align="right">
                       
                       <strong>
                       <asp:Label ID="Label22" runat="server" CssClass="style5" Font-Bold="False" 
                           style="font-family: Andalus; font-size: medium; color: #003300;" 
                           Text="Plant Code"></asp:Label>
                       </strong>
                       <br />
                       
                   </td>
                   <td width="55%"  align="left" >                      
                       <asp:DropDownList ID="ddl_PlantName" runat="server" Width="170px" 
                           CssClass="tb10" Height="25px" Font-Size="Small">
                       </asp:DropDownList>
                       <br />
                   </td>  
                                   
                   <td align="left" width="55%">
                       &nbsp;</td>
                                   
                   </tr>  
                   <tr>
                       <td width="5%">
                           <br />
                       </td>
                       <td align="right" width="30%">
                           <strong>
                           <asp:Label ID="Label23" runat="server" CssClass="style5" Font-Bold="False" 
                               Text="Date"></asp:Label>
                           </strong>
                       </td>
                       <td align="left" width="55%">
                           <asp:TextBox ID="txt_EntryDate" runat="server" CssClass="tb10" Height="25px" 
                               Width="170px"></asp:TextBox>
                           <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                               PopupButtonID="txt_EntryDate" PopupPosition="TopRight" 
                               TargetControlID="txt_EntryDate">
                           </asp:CalendarExtender>
                           <asp:DropDownList ID="ddl_GroupHeadName" runat="server" AutoPostBack="True" 
                               Height="25px" onselectedindexchanged="ddl_GroupHeadName_SelectedIndexChanged" 
                               Visible="False" Width="29px">
                           </asp:DropDownList>
                       </td>
                       <td align="left" width="55%">
                           &nbsp;</td>
             </tr>
             <tr>
                 <td class="style1" width="5%">
                 </td>
                 <td align="right" width="30%">
                     <strong>
                     <asp:Label ID="Label24" runat="server" CssClass="style5" Font-Bold="False" 
                         style="font-family: Andalus; font-size: medium" Text="Head Of Account"></asp:Label>
                     </strong>
                 </td>
                 <td align="left" width="55%">
                     <asp:DropDownList ID="ddl_HeadName" runat="server" AutoPostBack="true" 
                         CssClass="tb10" Height="25px" 
                         onselectedindexchanged="ddl_HeadName_SelectedIndexChanged" Width="170px" Font-Size="Small">
                     </asp:DropDownList>
                 </td>
                 <td align="left" width="55%">
                     &nbsp;</td>
             </tr>
             <tr>
                 <td class="style1" width="5%">
                 </td>
                 <td align="right" class="style1" width="30%">
                     <strong>
                     <asp:Label ID="Label25" runat="server" CssClass="style5" Font-Bold="False" 
                         style="font-family: Andalus; font-size: medium" Text="Type Of Ledger"></asp:Label>
                     </strong>
                 </td>
                 <td align="left"  width="55%">
                     <asp:DropDownList ID="ddl_SubHeadName" runat="server" CssClass="tb10" 
                         Height="25px" Width="170px" Font-Size="Small">
                     </asp:DropDownList>
                 </td>
                 <td align="left" class="style1" width="55%">
                     &nbsp;</td>
             </tr>
             <tr>
                 <td class="style1" width="5%">
                 </td>
                 <td align="right" width="30%">
                     <strong>
                     <asp:Label ID="Label26" runat="server" CssClass="style5" Font-Bold="False" 
                         style="font-family: Andalus; font-size: medium" Text="Type"></asp:Label>
                     </strong>
                 </td>
                 <td align="left" style="font-family: Andalus" width="55%">
                     <asp:DropDownList ID="ddl_Type" runat="server" CssClass="tb10" Height="25px" 
                         Width="170px" Font-Size="Small">
                          <asp:ListItem Value="Debit">Cash Payment</asp:ListItem>
                         <asp:ListItem Value="Credit">Cash Receipt</asp:ListItem> 
                         <asp:ListItem Value="Due">Iou</asp:ListItem>
                     </asp:DropDownList>
                 </td>
                 <td align="left" style="font-family: Andalus" width="55%">
                     &nbsp;</td>
             </tr>
             <tr>
                 <td width="5%" class="style2">
                 </td>
                 <td width="30%" class="style2">
                 </td>
                 <td align="left" width="55%" class="style2">
                     <asp:Panel ID="Panel1" runat="server" BorderColor="GrayText" BorderWidth="1" 
                         Width="210px" CssClass="tb10">
                         <asp:RadioButton ID="rd_Cash" runat="server" AutoPostBack="True" Checked="True" 
                             oncheckedchanged="rd_Cash_CheckedChanged" Text="Cash" />
                         <asp:RadioButton ID="rd_Cheque" runat="server" AutoPostBack="True" 
                             Enabled="False" oncheckedchanged="rd_Cheque_CheckedChanged" Text="Cheque" />
                         <asp:RadioButton ID="rd_Journal" runat="server" AutoPostBack="True" 
                             Enabled="False" oncheckedchanged="rd_Journal_CheckedChanged" Text="Journal" />
                     </asp:Panel>
                 </td>
                 <td align="left" class="style2" width="55%">
                 </td>
             </tr>
             <tr>
                 <td class="style1" width="5%">
                     &nbsp;</td>
                 <td align="right" width="30%">
                     <strong>
                     <asp:Label ID="Label27" runat="server" CssClass="style5" Font-Bold="False" 
                         style="font-family: Andalus; font-size: medium" Text="Ref Name"></asp:Label>
                     </strong>
                 </td>
                 <td align="left" width="55%">
                     <asp:TextBox ID="txt_Name" runat="server" CssClass="tb10" Height="25px" 
                         Width="182px"></asp:TextBox>
                 </td>
                 <td align="left" width="55%">
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                         ControlToValidate="txt_Name" ErrorMessage="*"></asp:RequiredFieldValidator>
                 </td>
             </tr>
             <tr>
                 <td width="5%">
                 </td>
                 <td align="right" width="30%">
                     <strong>
                     <asp:Label ID="Label29" runat="server" CssClass="style5" Font-Bold="False" 
                         style="font-family: Andalus; font-size: medium" Text="Amount"></asp:Label>
                     </strong>
                 </td>
                 <td align="left" width="55%">
                     <asp:TextBox ID="txt_Amount" runat="server" CssClass="tb10" Height="25px" 
                         Width="170px"></asp:TextBox>
                 </td>
                 <td align="left" width="55%">
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                         ControlToValidate="txt_Amount" ErrorMessage="*"></asp:RequiredFieldValidator>
                 </td>
             </tr>
             <tr>
                 <td width="5%">
                 </td>
                 <td align="right" width="50%">
                     <strong>
                     <asp:Label ID="Label30" runat="server" CssClass="style5" Font-Bold="False" 
                         style="font-family: Andalus; font-size: medium" Text="Narration"></asp:Label>
                     </strong>
                 </td>
                 <td align="left" width="55%">
                     <asp:TextBox ID="txt_Narration" runat="server" CssClass="tb10" Height="50px" 
                         TextMode="MultiLine" Width="220px"></asp:TextBox>
                 </td>
                 <td align="left" width="55%">
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                         ControlToValidate="txt_Narration" ErrorMessage="*"></asp:RequiredFieldValidator>
                 </td>
             </tr>
                   
                
                 
        
            
                  
                 
        
            
                   
                   <tr>
                       <td width="5%">
                           &nbsp;</td>
                       <td align="right" width="50%">
                           <strong>
                           <asp:Label ID="Lbl_Giver" runat="server" CssClass="style5" Font-Bold="False" 
                               style="font-family: Andalus; font-size: medium" Text="Giver"></asp:Label>
                           </strong>
                       </td>
                       <td align="left" width="55%">
                           <asp:DropDownList ID="ddl_Giver" runat="server" AutoPostBack="true" 
                               CssClass="tb10" Font-Size="Small" Height="25px" Width="170px">
                           </asp:DropDownList>
                       </td>
                       <td align="left" width="55%">
                           &nbsp;</td>
             </tr>
             <tr>
                 <td width="5%">
                     &nbsp;</td>
                 <td align="right" width="50%">
                     &nbsp;</td>
                 <td align="left" width="55%">
                     <asp:Button ID="btn_Ok" runat="server" BackColor="#6F696F" 
                         CssClass="button2222" ForeColor="White" Height="26px" onclick="btn_Ok_Click" 
                         style="text-align: center; " Text="Save" Width="70px" />
                 </td>
                 <td align="left" width="55%">
                     &nbsp;</td>
             </tr>
                   
                
                 
        
            
                  
                 
        
            
                   
                   </table>
                   </center>
                   <div >
                   <table border="0" id="Cheque" cellspacing="1" align="center">
                   <tr>
                   <td width="5%">
                   </td>
                   <td width="30%">
                       <strong>
                       <asp:Label ID="Lbl_ChequeDate" runat="server" CssClass="style5" 
                           Font-Bold="False" style="font-family: Andalus; font-size: medium" 
                           Text="ChequeDate"></asp:Label>
                       </strong>
                       </td>
                   <td width="55%">
                       <asp:TextBox ID="txt_ChequeDate" runat="server" CssClass="tb10" ></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                           PopupButtonID="txt_ChequeDate" PopupPosition="TopRight" 
                           TargetControlID="txt_ChequeDate">
                       </asp:CalendarExtender>
                   </td>
                 
                   </tr>
                    <tr>
                   <td width="5%">
                   </td>
                   <td width="30%">
                       <asp:Label ID="Lbl_favourof" runat="server" Text="Favour of" CssClass="style3"></asp:Label>
                   </td>
                   <td width="55%">
                       <asp:TextBox ID="txt_favourof" runat="server"  Width="182px" CssClass="tb10"></asp:TextBox>
                   </td>
                 
                   </tr>
                   <tr>
                   <td width="5%" class="style2">
                   </td>
                   <td width="30%" class="style2">
                       <asp:Label ID="Lbl_BankName" runat="server" Text="Bank Name" CssClass="style3"></asp:Label>
                   </td>
                   <td width="55%" class="style2">
                       <asp:TextBox ID="txt_BankName" runat="server"  Width="181px" CssClass="tb10"></asp:TextBox>
                   </td>
                  
                   </tr>
                   <tr>
                   <td width="5%">
                   </td>
                   <td width="30%">
                       <asp:Label ID="Lbl_ClearingDate" runat="server" Text="Clearing Date" 
                           CssClass="style3"></asp:Label>
                   </td>
                   <td width="55%">
                       <asp:TextBox ID="txt_ClearingDate" runat="server" CssClass="tb10" ></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                           PopupButtonID="txt_ClearingDate" PopupPosition="TopRight" 
                           TargetControlID="txt_ClearingDate">
                       </asp:CalendarExtender>
                   </td>
                  
                   </tr>
                   <tr>
                   <td width="5%">
                   </td>
                   <td width="30%">
                       &nbsp;</td>
                   <td width="55%">
                       <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" 
                           PopupButtonID="txt_ClearingDate" PopupPosition="TopRight" 
                           TargetControlID="txt_ClearingDate">
                       </asp:CalendarExtender>
                   </td>
                  
                   </tr>
                  
                  
                   </table>
                   </div>
                 <div align="left">
                     <asp:Label ID="Lbl_Errormsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
              </div>
                   </fieldset>
                   </center>

                  

                 <%--  </div>--%>
                    </center>
                   <br />                  
    
    <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
    </ContentTemplate>
        </asp:UpdatePanel>  
   </div>    
</asp:Content>
