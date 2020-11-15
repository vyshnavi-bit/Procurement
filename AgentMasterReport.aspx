<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgentMasterReport.aspx.cs" Inherits="AgentMasterReport" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

    
         .buttonclass
{
padding-left: 10px;
font-weight: bold;
}
.buttonclass:hover
{
color: white;
background-color:Orange;
}


.columnscss
{
width:25px;
font-weight:bold;
font-family:Verdana;
}




    .style1
    {
        width: 100%;
    }
    .style2
    {
        color: #990033;
        font-family: Andalus;
        font-size: medium;
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
 <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>


 <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>

   <center>  <span class="style2"><strong>Agent Information</strong></span><br /> </center>
    <table class="style1">
        <tr align=center>
            <td>
                        <asp:Panel ID="Panel2" runat="server">
                            <table class="style1">
                                <tr width=50%>
                                    <td align=right width=50%>
                                        &nbsp;</td>
                                    <td align="left" width=50%>
                                        &nbsp;</td>
                                </tr>
                                <tr width="50%">
                                    <td align="right" width="50%">
                                        <asp:Label ID="Label2" runat="server" 
                                            style="font-family: Andalus; font-size: medium; font-weight: 700;" 
                                            Text="Plant Code"></asp:Label>
                                    </td>
                                    <td align="left" width="50%">
                                        <asp:DropDownList ID="ddl_PlantName" runat="server" Font-Size="Small" 
                                            Height="30px" onselectedindexchanged="ddl_PlantName_SelectedIndexChanged" 
                                            Width="175px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr width="50%">
                                    <td align="right" width="50%">
                                        <table width="100%">
                                            <tr align="right">
                                                <td align="right"  width="15%">
                                                    <asp:Label ID="Label3" runat="server" 
                                                        style="font-family: Andalus; font-size: medium; font-weight: 700;" 
                                                        Text="From Date"></asp:Label>
                                                    <asp:TextBox ID="txt_FromDate" runat="server" Width="90px"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                        Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                                        TargetControlID="txt_FromDate">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                           
                                        </table>
                                    </td>



                                    <td align="left" width="50%">
                                        <asp:Label ID="Label4" runat="server" 
                                            style="font-family: Andalus; font-size: medium; font-weight: 700;" 
                                            Text="To Date"></asp:Label>
                                        <asp:TextBox ID="txt_ToDate" runat="server" Width="90px"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" 
                                            Format="dd/MM/yyyy" PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                                            TargetControlID="txt_ToDate">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr  align="right">
                                    <td>
                                        <asp:Button ID="btn_ok" runat="server" CssClass="buttonclass" 
                                            onclick="btn_ok_Click" Text="OK" />
                                    </td>
                                    <td align="left">
                                            
                                    <asp:Button ID="Button2" runat="server" 
                        BorderStyle="Inset" Font-Bold="True" 
                        OnClientClick="return PrintPanel();" Text="Print" CssClass="buttonclass" 
                        BorderColor="#663300" BorderWidth="1px" Height="23px" onclick="Button2_Click"  />
                                        <asp:Button ID="Button3" runat="server" CssClass="buttonclass" 
                                            onclick="Button3_Click" Text="Export" />
                                       </td>
                                </tr>
                            </table>
                        </asp:Panel>




                    </td>
        </tr>
    </table>
    <br />
    <table class="style1">
        <tr align=center>
            <td>


             <asp:Panel id="pnlContents" runat = "server" Height="2000px"> 
                
                 
                 <asp:GridView ID="GridView1" runat="server"  
                     HeaderStyle-BackColor=Silver HeaderStyle-ForeColor="Brown"
                     CssClass="GridViewStyle" BorderColor="#FF9900" 
                     onrowcreated="GridView1_RowCreated" 
                     onrowdatabound="GridView1_RowDataBound" HeaderStyle-HorizontalAlign="Center" 
                     Font-Size="Small">
                     <Columns>
                     <asp:TemplateField HeaderText="SNo" >
        <ItemTemplate >


                     <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
     </asp:TemplateField>


                     <%-- //   <asp:BoundField DataField="Type" HeaderText="CanType" SortExpression="Type" />--%>
                     </Columns>
                     <HeaderStyle BackColor="Silver" ForeColor="Brown" />
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
                 <br />
                 <br />
                </asp:panel>
                  </div>
             
            </td>
        </tr>
    </table>
     </ContentTemplate>


        <Triggers>
<asp:PostBackTrigger ControlID="Button3" />
</Triggers>
        </asp:UpdatePanel>
   

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
<br />
<br />
<br />
<br />
   

        <br />
        

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

