<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProcurementDataBulkBackup.aspx.cs" Inherits="ProcurementDataBulkBackup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
        {
            height: 24px;
        }
                
             .style103
        {
            width: 400px;
        }
        
        .style104
        {
            height: 22px;
        }
        
    </style>
  



    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
<form>

<script type="text/javascript">
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

  
   <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height:100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>

<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"    >
            <ContentTemplate>





<body>
<html>
    <div>
<center>


    <br />


    <asp:Label ID="Label1" runat="server" Text="PROCUREMENT   DATA BACKUP" 
        style="font-family: serif"></asp:Label>

    <br />

</center>

</div>
<div  align="center">

<center style="border-style: none">
<asp:Panel ID="Panel1"  Width="49%" runat="server"    BorderWidth="2px">

   <center>

    <table  class="style103" cellspacing="2">
        <tr align="left">
            <td width=28% class="style104">


                </td>
            <td width="28%" class="style104">
                </td>
        </tr>
        <tr align="right">
            <td width="28%">
                <asp:Label ID="Label23" runat="server" style="font-family: serif" 
                    Text="From Date"></asp:Label>
            </td>
            <td align="left" width="28%">
                <asp:TextBox ID="txt_frmdate" runat="server" CssClass="tb10" Height="20px" 
                    TabIndex="4" Width="100px"></asp:TextBox>
                <asp:CalendarExtender ID="txt_frmdate_CalendarExtender" runat="server" 
                    Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                    TargetControlID="txt_frmdate">
                </asp:CalendarExtender>
            </td>
        </tr>
        </table>
       <table cellspacing="2" class="style103">
           <tr align="left">
               <td class="style4">
                   &nbsp;</td>
               <td class="style4">
                   &nbsp;</td>
               <td  align="center">
                    <asp:Button ID="Button1" runat="server" Text="BackUp" 
                    BackColor="Green" BorderStyle="Double"
                                        Font-Bold="True" ForeColor="White" Height="26px" 
                    Width="75px"
                                        OnClick="Button1_Click" 
                        onclientclick="return confirmation();" />
               </td>
               <td class="style4">
                   &nbsp;</td>
           </tr>
           <tr align="CENTER">
               <td>
                   &nbsp;</td>
               <td>
                   &nbsp;</td>
               <td align="center">
                   <asp:Label ID="msg_lbl" runat="server" Text="Label"></asp:Label>
               </td>
               <td>
                   &nbsp;</td>
           </tr>
           <tr align="left">
               <td>
                   &nbsp;</td>
               <td>
                   &nbsp;</td>
               <td>
                   &nbsp;</td>
               <td>
                   &nbsp;</td>
           </tr>
       </table>
    </center>
   
   </asp:Panel>








    <br />
  </center>
</div>





   </asp:Panel>
       <br />
    </center>
   
</div>
 </ContentTemplate>
              </asp:UpdatePanel>
</form>
</body>
</html>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
