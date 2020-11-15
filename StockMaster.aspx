<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockMaster.aspx.cs" Inherits="StockMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style3
        {
            text-align:left;
             width: 125px;
            
        }
        .style4
        {
            text-align: left;
        }
        .style5
        {
            font-size: medium;
            font-family: Andalus;
        }
        .style6
        {
            font-size: medium;
            color: #000000;
            font-family: Andalus;
        }
        .style7
        {
            width: 431px;
        }
        .style8
        {
            width: 350px;
        }
        .style9
        {
            width: 100%;
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

        <center>    
            <table class="style9">
                <tr>
                    <td align="left">
                        &nbsp;</td>
                    <td align="left">
                        <strong>    
         <center>   
             <asp:Label ID="Label5" runat="server" Text="Stock Master " 
        CssClass="style5"></asp:Label>   </strong>
        </center>
                    </td>
                </tr>
                </table>
    </center>
  <center>   <div>
    <br />

    <center class="style7">
        <fieldset width="25%">
    
       <table class="style8" align="center">
           <tr>
               <td class="style3">
                   <asp:Label ID="Label1" runat="server" CssClass="style6" Text="Tid" 
                       Visible="False"></asp:Label>
                   </td>
   <center> 
               <td class="style4">
                   <asp:TextBox ID="txt_tid" runat="server" Visible="False"></asp:TextBox>
       </td>
           </tr>
    </center>
           <tr>
               <td class="style3">
                   <asp:Label ID="Label2" runat="server" CssClass="style6" Text="Stock Type"></asp:Label>
               </td>
   <center> 
               <td class="style4">
                   <asp:DropDownList ID="ddl_stocktype" runat="server" Font-Bold="true" 
                       Font-Size="12px" Width="140px" AutoPostBack="True" 
                       onselectedindexchanged="ddl_stocktype_SelectedIndexChanged">
                         <asp:ListItem>------Select------</asp:ListItem>
                          <asp:ListItem Value="1">Feed</asp:ListItem>
                          <asp:ListItem Value="2">Billadv</asp:ListItem>
                          <asp:ListItem Value="3">Meterials</asp:ListItem>
                          <asp:ListItem Value="4">Can</asp:ListItem>      
                          <asp:ListItem Value="5">Dpu</asp:ListItem>
                          <asp:ListItem Value="6">Others</asp:ListItem>
                         <asp:ListItem Value="7">Water</asp:ListItem>
                   </asp:DropDownList>
       </td>
           </tr>
           <tr>
               <td class="style3">
                   <asp:Label ID="Label6" runat="server" CssClass="style6" Text="Stock Category"></asp:Label>
               </td>
               <td class="style4">
                   <asp:TextBox ID="txt_stockname" runat="server"  Width="140px"
                       ontextchanged="txt_stockname_TextChanged" AutoPostBack="True"></asp:TextBox>
       </td>
           </tr>
           <tr>
               <td class="style3">
                   <asp:Label ID="Label3" runat="server" CssClass="style6" Text="Stock Weight" 
                       Visible="False"></asp:Label>
                   </td>
   <center> 
               <td class="style4">
                   <asp:TextBox ID="txt_stockweight" runat="server" AutoPostBack="True" 
                       ontextchanged="txt_stockweight_TextChanged" Visible="False"></asp:TextBox>
       </td>
           </tr>
           <tr>
               <td class="style3">
                   <asp:Label ID="txt_ddeddate" runat="server" CssClass="style6" Text="Added Date" 
                       Visible="False"></asp:Label>
                   </td>
   <center> 
               <td class="style4">
                   <asp:TextBox ID="txt_FromDate" runat="server" Enabled="False" Visible="False"></asp:TextBox>
       </td>
           </tr>
           <tr>
               <td class="style3" colspan="2">
                    <asp:Label ID="lblmsg" runat="server" style="font-family: serif"></asp:Label>
                    </td>
           </tr>
           </table>
    
    
    </fieldset>
        <br />
    </center>
  
    </div>
   </center>
    <div>
  <center>    
      <asp:Button ID="Button1" runat="server" Text="Save"  ForeColor="White"  Font-Bold="True"   
       BackColor="#006699" onclick="Button1_Click" />
       <asp:Button ID="btn_print" runat="server" BackColor="#006699" ForeColor="White" 
                            Text="Print" Height="26px" 
                 Font-Bold="True"   OnClientClick = "return PrintPanel();" />
      <br />
      <br />
      <asp:Panel id="pnlContents" runat = "server">   
      <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
          BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
         Font-Size="12px" onpageindexchanging="GridView1_PageIndexChanging"  
          AutoGenerateColumns="False" onrowcancelingedit="GridView1_RowCancelingEdit" 
          PageSize="30">
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
              <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" />
              <asp:BoundField DataField="StockGroup" HeaderText="StockType" 
                  SortExpression="StockGroup" />
              <asp:BoundField DataField="StockSubGroup" HeaderText="StockSubGroup" 
                  SortExpression="StockSubGroup" />
              <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" 
                  SortExpression="AddedDate" />
          </Columns>
      </asp:GridView>
       </asp:Panel>
      <br />
        </center>  
    </div>

      </ContentTemplate>
              </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

