<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantBankPaymentSelectedList.aspx.cs" Inherits="PlantBankPaymentSelectedList" %>

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
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;BankPayment SelectedList</p>
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
                      <tr>
                          <td width="18%">
                              &nbsp;</td>
                          <td class="fontt" width="10%">
                              &nbsp;</td>
                          <td align="right" class="fontt" width="15%">
                              <asp:Label ID="lbl_PlantName" runat="server" Text="Plant Name"></asp:Label>
                          </td>
                          <td width="10%">
                              <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
                                  Font-Bold="True" Font-Size="Large" 
                                  onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="190px">
                              </asp:DropDownList>
                          </td>
                          <td class="fontt" width="9%">
                              &nbsp;</td>
                          <td align="right" class="fontt" width="15%">
                              &nbsp;</td>
                          <td width="12%">
                              <asp:Label ID="lbl_Plantcode" runat="server" Text="Plant ID" Visible="False"></asp:Label>
                          </td>
                          <td width="20%">
                              <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                  Font-Bold="True" Font-Size="Large" 
                                  onselectedindexchanged="ddl_Plantcode_SelectedIndexChanged" Visible="false" 
                                  Width="190px">
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr align=center>
                          <td colspan="8">
                              <asp:GridView ID="GridView7" runat="server" 
                                  onrowdatabound="GridView7_RowDataBound" 
                                  onselectedindexchanged="GridView7_SelectedIndexChanged" 
                                  style="text-align: center">
                                  <Columns>
                                      <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                                  </Columns>
                              </asp:GridView>
                              <asp:GridView ID="GridView8" runat="server" 
                                  CssClass="table table-striped table-bordered table-hover" 
                                  onrowdatabound="GridView8_RowDataBound" ShowFooter="true">
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
                              <br />
                              <asp:Button ID="Button2" runat="server" CssClass="button" Font-Size="Smaller" 
                                  onclick="Button2_Click" Text="Export" />
                          </td>
                      </tr>
                   </table>
   </div>
   <br />
   



     <div  class="disnone">
        <table cellpadding="0" cellspacing="5" class="tableInfo">
            <tr valign=top>
                <td valign=top align="center"> 
                    <asp:Label ID="lbl_PlantName0" runat="server" Text="Plant Name"></asp:Label>
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
              <%--  <asp:PostBackTrigger  ControlID="btn_Submit"  />
                 <asp:PostBackTrigger  ControlID="btn_ok"  />
                  <asp:PostBackTrigger  ControlID="btn_Export"  />
                   <asp:PostBackTrigger  ControlID="btn_Exportcsv"  />
                     <asp:PostBackTrigger  ControlID="btn_SbipaymentListcsv0"  />
                       <asp:PostBackTrigger  ControlID="btn_ExportIng" />
                         <asp:PostBackTrigger  ControlID="btn_ExportIngcsv"   />
                            <asp:PostBackTrigger  ControlID="btn_Hdfc"   />
                            --%>
                               <asp:PostBackTrigger  ControlID="Button2"   />
            </Triggers>
   </asp:UpdatePanel>  

        </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

