<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgentsProcurementList.aspx.cs" Inherits="AgentsProcurementList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style5
        {
            font-size: medium;
            font-family: Andalus;
        }
        .style9
        {
            width: 100%;
        }
    
        .style10
        {
            font-family: Andalus;
        }
         .style101
        {
           width:75%;
        }
        .Grid
        {
            border: 1px solid #525252;
        }
        .Grid th
        {
            border: 1px solid #525252;
        }
        .Grid th
        {
            color: #fff;
            background-color: #3AC0F2;
        }
        
        .buttonclass
{
padding-left: 10px;
font-weight: bold;
}
        .button
        {
            font-weight: 700;
        }
        
    </style>

    <script type = "text/javascript">
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
 <div style="position: fixed; text-align: center; height:1%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
              
    
        <center>    
            <table class="style9">
                <tr>
                    <td align="left">
                        &nbsp;</td>
                    <td align="left">
                        <strong>    
         <center>   
             <asp:Label ID="Label5" runat="server" Text="Agent Procurement List" 
        CssClass="style5"></asp:Label>   </strong>
        </center>
                    </td>
                </tr>
                <tr align=center width="60%"  >
                    <td align="center" colspan="2">
                    <fieldset class=style101>
                    
                        <table class="style9">
                            <tr>
                                <td>

                                    &nbsp;</td>
                                <td align="center">
                      
                     <asp:Label ID="lbl_Plantname" runat="server" Text="Plantname" style="font-family: Andalus" ></asp:Label>
                      
          <asp:DropDownList ID="ddl_Plantname" runat="server" Width="150px" AutoPostBack="True" 
                                        onselectedindexchanged="ddl_Plantname_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:CheckBox ID="Route" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="Route_CheckedChanged" style="font-family: Andalus" 
                                        Text="RouteWise" />
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="2">
                                    <asp:Label ID="lbl_route" runat="server" style="font-family: Andalus" 
                                        Text="Routename"></asp:Label>
                                    <asp:DropDownList ID="ddl_RouteName" runat="server" 
                                        onselectedindexchanged="ddl_routename_SelectedIndexChanged" Width="150px">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddl_RouteID" runat="server" AutoPostBack="True" 
                                        Font-Bold="False" Visible="false" Width="30px">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                        Height="16px" Visible="false" Width="29px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr align="center">
                                <td class="style10" colspan="2">
                                    <asp:RadioButtonList ID="rdocheck" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1">Sessions</asp:ListItem>
                                        <asp:ListItem Value="2">PerDay</asp:ListItem>
                                        <asp:ListItem Value="3">Period</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr align="center">
                                <td class="style10" colspan="2">
                                    <asp:Label ID="lbl_Sess" runat="server" style="font-family: Andalus" 
                                        Text="Session"></asp:Label>
                                    <asp:DropDownList ID="ddl_Sessions" runat="server" 
                                        Width="150px">
                                        <asp:ListItem Value="-1">------Select------</asp:ListItem>
                                        <asp:ListItem Value="1">AM</asp:ListItem>
                                        <asp:ListItem Value="2">PM</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr align="center">
                                <td class="style10" colspan="2">

                     <asp:Label ID="lbl_frmdate" runat="server" Text="FromDate" style="font-family: Andalus" ></asp:Label>
                                    <asp:TextBox ID="txt_FromDate" runat="server" Height="25px" 
                                        style="font-family: Andalus; font-size: small" Width="170px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" 
                                        PopupButtonID="txt_FromDate" PopupPosition="BottomRight" 
                                        TargetControlID="txt_FromDate">
                                    </asp:CalendarExtender>
                                    <asp:Label ID="lbl_todate" runat="server" style="font-family: Andalus" 
                                        Text="ToDate"></asp:Label>
                                    <asp:TextBox ID="txt_ToDate" runat="server" 
                                        style="font-size: small; font-family: Andalus" Width="170px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" 
                                        PopupButtonID="txt_ToDate" PopupPosition="BottomRight" 
                                        TargetControlID="txt_ToDate">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            </table>
                    
                    </fieldset>

                       </td>
                </tr>
                </table>
    </center>
  <center>   <div>
      <asp:Label ID="lblmsg1" runat="server" style="font-family: serif"></asp:Label>
      <br />
      <asp:Button ID="Button1" runat="server" CssClass="buttonclass" 
          onclick="Button1_Click" Text="Show" />
      <asp:Button ID="Button2" runat="server" CssClass="buttonclass" Font-Bold="True" 
          Height="23px" OnClientClick="return PrintPanel();" Text="Print" />
      <asp:Button ID="Button3" runat="server" CssClass="buttonclass" 
          onclick="Button3_Click" Text="Export" />
      <br />

    </div>
   </center>
    <div>
     <asp:Panel id="pnlContents" runat = "server"> 
  <center>    
      <div align="right">
          <table class="style9">
              <tr align="center">
                  <td>
                  <div align="center">
                      <br />
                      <center>
                      <table width="75%">
                          <tr align="center" >
                              <td>
                                  <asp:Image ID="Image1" runat="server" Height="75px" ImageAlign="Middle" 
                                      ImageUrl="~/Image/VLogo.png" Width="75px" />
                              </td>
                          </tr>
                          <tr align="center">
                              <td>
                                  <asp:Label ID="Label8" runat="server" 
                                      style="font-weight: 700; font-family: Andalus; color: #990033;" 
                                      Text="SRI VYSHNAVI DAIRY SPECIALITIES"></asp:Label>
                              </td>
                          </tr>
                          <tr align="center">
                              <td>
                                  <asp:Label ID="Label14" runat="server" 
                                      style="font-weight: 700; font-family: Andalus; color: #990033;" 
                                      Text="Agent Procurement Details"></asp:Label>
                              </td>
                          </tr>
                          <tr align="center">
                              <td>
                                  <asp:Label ID="lbldisplay" runat="server" 
                                      style="font-weight: 700; font-family: Andalus; color: #990033;"></asp:Label>
                              </td>
                          </tr>
                      </table>
                      </center>
   </div>

                     </td>
              </tr>
          </table>
      </div>
      <div align="center" >
      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True"  CssClass="Grid"  
              PageSize="20" ShowFooter="true"  Font-Size="12px" 
              onrowdatabound="GridView1_RowDataBound"  >

               <Columns>
                                                   

                                                    
									   <asp:TemplateField HeaderText="SNo.">
										   <ItemTemplate>
											   <%# Container.DataItemIndex + 1 %>
										   </ItemTemplate>
									   </asp:TemplateField>
								 


                                                   
                                                   
                                                   </Columns>


</asp:GridView>
   <br />
      </div>    
      <br />
        </center> 
           </asp:panel> 
    </div>
           </left>
                </ContentTemplate>
                 <Triggers>
<asp:PostBackTrigger ControlID="Button3" />
</Triggers>
            </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
