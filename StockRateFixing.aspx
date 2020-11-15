<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockRateFixing.aspx.cs" Inherits="StockRateFixing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: center;
            width: 896px;
        }
        .style13
        {
            font-size: medium;
            color: #000000;
            font-family: Andalus;
        }
        .style16
        {
            text-align: left;
        }
        .style18
        {
            text-align: right;
        }
        .style19
        {
            width: 344px;
        }
        .style5
        {
            font-size: medium;
            font-family: Andalus;
        }
        .gridview1
        {}
        .style20
        {
            text-align: right;
            height: 26px;
        }
        .style21
        {
            text-align: left;
            height: 26px;
        }
        .style2
        {
            font-family: Andalus;
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
 <div style="position: fixed; text-align: center; height:10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>


<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"    >
            <ContentTemplate>


    <strong>    
        <center>   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   <asp:Label ID="Label5" 
                runat="server" Text="Stock or RateUpdate " 
        CssClass="style5"></asp:Label>  </center>
    </strong>     


    <div class="style18"> 
     
         <br />
         <center>   
         <fieldset style="background-color: #FFFFFF; width:50%;" 
                 style="height: 200px">  
     <center>
 
            <table align="center" width:"50%" class="style19">
               
                    <tr width=20%>
                    <td align="right">

        <asp:Label ID="lbl_TruckId13" runat="server" Text="Stock To" CssClass="style2"></asp:Label>

                    </td>
                    <td class="style16">

                <asp:DropDownList ID="ddl_stockto" runat="server" Height="30px" 
                    Width="175px" Font-Bold="true" Font-Size="12px"
                 
                    AutoPostBack="True" onselectedindexchanged="ddl_stockto_SelectedIndexChanged">
                    <asp:ListItem Value="-1">-----------Select------------</asp:ListItem>
                    <asp:ListItem Value="1">To Godown</asp:ListItem>
                    <asp:ListItem Value="2">To Plant</asp:ListItem>
                </asp:DropDownList>
                    </td>
                </tr>
                <tr width=20%>
                    <td align="right">

        <asp:Label ID="lbl_TruckId12" runat="server" Text="Plant Code" CssClass="style2"></asp:Label>

                    </td>
                    <td class="style16">

                <asp:DropDownList ID="ddl_PlantName" runat="server" Height="30px" 
                    Width="175px" Font-Bold="true" Font-Size="12px" AutoPostBack="True" 
                            onselectedindexchanged="ddl_PlantName_SelectedIndexChanged" >
                </asp:DropDownList>
                    </td>
                </tr>
                <tr width=20%>
                    <td align="right">
                   <strong>
                   <asp:Label ID="Label2" runat="server" CssClass="style13" Text="Stock Type"></asp:Label>
                   </strong>
                    </td>
                    <td class="style16">

              <asp:DropDownList ID="ddl_materialName" runat="server" Font-Bold="true" Font-Size="12px"
                 Height="30px" Width="175px" 
                  AutoPostBack="True" 
                  onselectedindexchanged="ddl_materialName_SelectedIndexChanged">
              </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td class="style18">
                   <strong>
                   <asp:Label ID="Label6" runat="server" CssClass="style13" Text="Stock Category "></asp:Label>
                   </strong>
                    </td>
                    <td class="style16">

               <asp:DropDownList ID="ddl_materialName1" runat="server" 
                   Font-Bold="true" Font-Size="12px" Height="30px" Width="175px" 
                   AutoPostBack="True" onselectedindexchanged="ddl_materialName1_SelectedIndexChanged">
               </asp:DropDownList>

                    </td>
                </tr>
                <tr>
                    <td class="style18">
                   <strong>
                   <asp:Label ID="Label4" runat="server" CssClass="style13" Text="Rate"></asp:Label>
                   </strong>
                    </td>
 <center>  
     <td class="style16">
                   <asp:TextBox ID="txt_stockrate" runat="server" CssClass="tb10" Height="24px" 
                       Width="140px">0</asp:TextBox>
              </td>
                    </tr>
                </center>
                <tr>
                    <td class="style20">
                   <strong>
                   <asp:Label ID="txt_ddeddate" runat="server" CssClass="style13" 
                       Text="Added Date" Visible="False"></asp:Label>
                   </strong>
                    </td>
 <center>  
     <td class="style21">
                   <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" 
                       Font-Size="X-Small" Height="24px" TabIndex="4" Width="145px" 
                       Visible="False"></asp:TextBox>
                   <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                       Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                       TargetControlID="txt_FromDate">
                   </asp:CalendarExtender>
              </td>
                    </tr>
                    <tr>
                        <td width=30%>
                   <asp:DropDownList ID="ddl_Plantcode" runat="server" Height="16px" Width="126px" 
                       Visible="False">
                       <asp:ListItem>------Select------</asp:ListItem>
                       <asp:ListItem>Can</asp:ListItem>
                       <asp:ListItem>Feed</asp:ListItem>
                       <asp:ListItem>Medicine</asp:ListItem>
                       <asp:ListItem>Meterials</asp:ListItem>
                   </asp:DropDownList>
                        </td>
                    </center>
                    <td width=30%>
   <left>   
       <asp:Button ID="Button1" runat="server" Text="Save" BackColor="#006699" ForeColor="White" Font-Bold="True"  Font-Size="12px" Height="26px" 
         onclick="Button1_Click" />   
          <asp:Button ID="btn_print" runat="server" BackColor="#006699" ForeColor="White" Font-Size="12px" 
                            Text="Print" Height="26px"  
                 Font-Bold="True"   OnClientClick = "return PrintPanel();" />
         </left>
                    </td>
                </tr>
            </table>
 </center>
   </fieldset>  </center> </div> 






    <div WIDTH="100%">





        <asp:Label ID="lblmsg" runat="server" Text="lblmsg" ForeColor="Red"
            style="font-family: Andalus"></asp:Label>





        <br />

 <asp:Panel id="pnlContents" runat = "server">
   
   <center> 
       <br />
      <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
          onpageindexchanging="GridView1_PageIndexChanging" 
           AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
           BorderStyle="None" BorderWidth="1px" CellPadding="3" 
           Font-Size="12px"  ShowFooter="true" PageSize="20"  >
             <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
          <Columns>
               <asp:TemplateField HeaderText="SNo">
                               <ItemTemplate>
                                   <%#Container.DataItemIndex + 1 %>
                               </ItemTemplate>
                           </asp:TemplateField>
              <asp:BoundField DataField="StockGroup" HeaderText="StockType" 
                  SortExpression="StockGroup" />
              <asp:BoundField DataField="StockSubGroup" HeaderText="Stock Category " 
                  SortExpression="StockSubGroup" />
              <asp:BoundField DataField="ItemRate" HeaderText="StockRate" 
                  SortExpression="ItemRate" />
              <asp:BoundField DataField="Fixstatus" HeaderText="Status" 
                  SortExpression="Fixstatus" />
                  <asp:BoundField DataField="RateTypePlantorgodown" HeaderText="Mode" 
                  SortExpression="RateTypePlantorgodown" />
                    <asp:BoundField DataField="Plant_Code" HeaderText="Plant_Code" 
                  SortExpression="Plant_Code" />
              <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" 
                  SortExpression="AddedDate" />
          </Columns>
      </asp:GridView>
       <br />
      </center> 
       </asp:Panel>
    </div>

      </ContentTemplate>     

        </asp:UpdatePanel>
    


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

