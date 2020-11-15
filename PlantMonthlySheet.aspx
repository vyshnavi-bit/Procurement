<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantMonthlySheet.aspx.cs"     Inherits="PlantMonthlySheet"  EnableEventValidation="false" %> 
<%@ register Assembly="Ajaxcontroltoolkit" namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        
   
        
        
      
        
        
        
        
    .buttonclass
{
padding-left: 10px;
font-weight: bold;
}
        
        
        
        
        
        .style4
        {
            color: #990033;
        }
        
        
        
        
        
               .style2
               {
                   font-family: Andalus;
                   font-size: medium;
                   color: #000000;
               }
        
                       
        
        
        
    </style>


    <script type="text/javascript">
        function Showalert() {
            alert('Call JavaScript function from codebehind');
        }
</script>
    


      
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>




    <br />
                <table class="style3" __designer:mapid="7c3">
                    <tr __designer:mapid="7c4">
                        <td align="right" width=50% __designer:mapid="7c5">
                            <asp:Label ID="Label9" runat="server" CssClass="style2" Text="Plant Name"></asp:Label>
                            <asp:DropDownList ID="ddl_PlantName" runat="server" CssClass="tb10" 
                                Height="25px" Width="130px" Font-Size="Small">
                            </asp:DropDownList>
                        </td>
                        <td align="left" __designer:mapid="7c7">
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                oncheckedchanged="CheckBox1_CheckedChanged" 
                                style="font-family: Andalus; font-size: medium" Text="Show All Plant" />
                        </td>
                    </tr>
                    <tr __designer:mapid="7c9">
                        <td align="right" __designer:mapid="7ca">
                            <asp:Label ID="Label11" runat="server" CssClass="style2" Text="From Date"></asp:Label>
                            <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" Font-Size="Small" 
                                Height="20px" Width="125px"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="BottomRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                          
                        </td>
                        <td align=left __designer:mapid="7ce">
                            <asp:Label ID="Label10" runat="server" CssClass="style2" Text="To Date"></asp:Label>
                            <asp:TextBox ID="txt_ToDate" runat="server" CssClass="tb10" Font-Size="Small" 
                                Height="20px" Width="125px"></asp:TextBox>
                                  <asp:CalendarExtender ID="txt_ToDate_CalendarExtender2" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_ToDate" PopupPosition="BottomRight" 
                                TargetControlID="txt_ToDate">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    </table>




    <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height: 1%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>


 <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel2" runat="server"  >
            <ContentTemplate>

    
     <asp:Panel id="pnlContents" runat = "server">
    <table class="style3">
        <tr align="center">
            <td>
                <span class="style4"><strong>PLANT&nbsp;MONTHLY&nbsp; REPORT</strong></span>
                <br />
                <asp:Image ID="Image1" runat="server" Height="75px" 
                    ImageUrl="~/Image/VLogo.png" Width="75px" />
            

                <br />
                &nbsp;<asp:Button ID="Button2" runat="server" BackColor="#00CCFF" 
                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="30px" 
                    onclick="Button2_Click" Text="SHOW" Width="70px" />
                <asp:Button ID="btn_print" runat="server" BackColor="#00CCFF" 
                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="30px" 
                    OnClientClick="return PrintPanel();" Text="PRINT" />
            

                <asp:Button ID="Button3" runat="server" BackColor="#00CCFF" 
                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="30px" 
                  OnClick="GetSelectedRecords" Text="SELECT PLANT" Width="113px" />
            

                <asp:Button ID="btn_export" runat="server" BackColor="#00CCFF" 
                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="30px" 
                    onclick="btn_export_Click" Text="Export" Visible="true" />
            

            </td>
        </tr>
        <tr align="center">
            <td>
                <asp:GridView ID="GridView1" runat="server" CssClass="gridview1" 
                    Font-Size="12px" onrowdatabound="GridView1_RowDataBound" ShowFooter="true">
                    <Columns>

                        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SNo.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                    

                    </Columns>
                </asp:GridView>

                   <asp:GridView ID="GridView2" runat="server" CssClass="gridview1"  
                    onrowdatabound="GridView2_RowDataBound"  Font-Size="12px" 
                    onrowcreated="GridView2_RowCreated">


                    <Columns>

                        <%--<asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" Enabled="false"  />
            </ItemTemplate>
        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="SNo.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                    

                    </Columns>


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
            </td>
        </tr>
    </table>


    </asp:Panel> 
          </ContentTemplate>
       
          <Triggers>
<asp:PostBackTrigger ControlID="btn_export" />
</Triggers>

 </asp:UpdatePanel>



</asp:Content>

