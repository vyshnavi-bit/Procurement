<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantstockEntry.aspx.cs" Inherits="PlantstockEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
.style1
{
width:400px
}
    .style2
    {
        width: 100%;
    }   
    
    input[type=text],textarea
      {
      border: 1px solid #ccc;
      border-radius: 3px;
      box-shadow: inset 0 1px 2px rgba(0,0,0,0.1);
      width:200px;
      min-height: 28px;
      padding: 4px 20px 4px 8px;
      font-size: 12px;
      -moz-transition: all .2s linear;
      -webkit-transition: all .2s linear;
      transition: all .2s linear;
      }
 input[type=text]:focus,textarea:focus
      {
      width: 400px;
      border-color: #51a7e8;
      box-shadow: inset 0 1px 2px rgba(0,0,0,0.1),0 0 5px rgba(81,167,232,0.5);
      outline: none;
      }
 input[type="text"]:hover{
        outline: none;
        box-shadow: 0px 0px 5px #61C5FA;
        border:1px solid #5AB0DB;
        border-radius:0;

}    
    .style3
    {
        height: 26px;
    }
    
    .style4
    {
        font-size: medium;
    }
    
</style>

 <script language="javascript" type="text/javascript">
        function validate()
         {
            var summary = "";
          
            summary += isvalidLocation();
            summary + =isvaliddesc();
            if (summary != "") {
                alert(summary);
                return false;
            }
            else {
                return true;
            }
        }
       
        function isvalidLocation() {
            var uid;
            var temp = document.getElementById("<%=txt_qty.ClientID %>");
            uid = temp.value;
            if (uid == "") {
                return ("Please Enter Stock Qty" + "\n");
            }
            else {
                return "";
            }
        }


        function isvaliddesc()
         {
            var uid;
            var temp = document.getElementById("<%=txt_rate.ClientID %>");
            uid = temp.value;
            if (uid == "") {
                return ("Please Enter Stock Rate" + "\n");
            }
            else {
                return "";
            }
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
    <div>
  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
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
            <div align="right">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="fontt" 
                    NavigateUrl="~/DeductionEntry.aspx">Report</asp:HyperLink>
                </div>
   <center>
   <br />
    <fieldset   class="style1" >
    
    
        <table class="style2"  bgcolor="White">
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="Label5" runat="server" 
                        style="font-family: Andalus; " Text="Godown  Stock Entry" 
                        CssClass="style4"></asp:Label>
                    
                    </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label1" runat="server" 
                        style="font-family: Andalus; " Text="Stock Type" CssClass="style4"></asp:Label>
                    
                    </td>
                <td align="left">
                    <asp:DropDownList ID="ddl_materialName" runat="server" class="input" 
                        Width="175px" Height="30px" Font-Size="12px" Font-Bold="true" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddl_materialName_SelectedIndexChanged">
                        <asp:ListItem Value="-1">--------Select----------</asp:ListItem>
                        <asp:ListItem Value="1">Feed</asp:ListItem>
                        <asp:ListItem Value="2">Can</asp:ListItem>
                        <asp:ListItem Value="3">Medicine</asp:ListItem>
                        <asp:ListItem Value="4">LactoMeter</asp:ListItem>
                        <asp:ListItem Value="5">Dpu Machine</asp:ListItem>
                        <asp:ListItem Value="6">Others</asp:ListItem>
                        <asp:ListItem Value="7">Water</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label2" runat="server" 
                        style="font-family: Andalus; " Text="Stock Name" CssClass="style4"></asp:Label>
                </td>
                <td align="left">
                               
                    
                    <asp:DropDownList ID="ddl_materialType" runat="server" class="input" 
                        Width="175px" Font-Size="12px" Font-Bold="true">
                    </asp:DropDownList>
                    
                    
                    </td>
            </tr>
            <tr>
                <td align="right" class="style3">
                    <asp:Label ID="Label3" runat="server" 
                        style="font-family: Andalus; " Text="No of Qty" CssClass="style4"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_qty" runat="server" class="input" Font-Bold="True" 
                        Font-Size="Medium" ID:NAME="" CssClass="tb10" Height="20px" 
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="font-family: Andalus">
                    <asp:Label ID="Label4" runat="server" 
                        style="font-family: Andalus; " Text="Rate" CssClass="style4" 
                        Visible="False"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txt_rate" runat="server" class="input" Font-Bold="True" 
                        Font-Size="Medium" ID:NAME="" Height="20px" Width="150px" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td align="left">
                    <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" BackColor="#006699" ForeColor="White" Font-Size="12px" Font-Bold="true"
                        OnClientClick="return validate();" Text="Submit" style="height: 26px" />
                   
                    <asp:Button ID="btn_print" runat="server" BackColor="#006699" Font-Bold="True" 
                        ForeColor="White" Height="26px" OnClientClick="return PrintPanel();" 
                        Text="Print" />
                   
                    <asp:Button ID="btnedit" runat="server" onclick="btnedit_Click" Text="Edit" BackColor="#006699" ForeColor="White" Font-Size="12px" Font-Bold="true"   Height="26px" Visible="false"/>
                   
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblmsg" runat="server" style="font-family: serif"></asp:Label>
                    <br />
                    <asp:Label ID="lblmsg1" runat="server" style="font-family: serif"></asp:Label>
                </td>
            </tr>
        </table>
    
    
    </fieldset >
       <br />
       <br />
       <asp:Panel id="pnlContents" runat = "server">   
       <table class="style2">
           <tr>
               <td align="center">
                   <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                       AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
                       BorderStyle="None" BorderWidth="1px" CausesValidation="false" 
                       CellPadding="3" Font-Size="12px" 
                       onpageindexchanging="GridView1_PageIndexChanging" 
                       PageSize="20">
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
                           
                           <asp:BoundField DataField="GHeader" HeaderText="GHeader" 
                               SortExpression="GHeader" ReadOnly="True" />
                           <asp:BoundField DataField="GSHeader" HeaderText="GSHeader" 
                               SortExpression="GSHeader" ReadOnly="True" />
                           <asp:BoundField DataField="qty" HeaderText="qty" SortExpression="qty" >
                           <ControlStyle Width="100px" />
                           </asp:BoundField>
                           <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" 
                               SortExpression="AddedDate" ReadOnly="True" />
                           <asp:CommandField ShowEditButton="True" />
                       </Columns>
                   </asp:GridView>
               </td>
           </tr>
       </table>
        </asp:Panel>
       <br />
    </center>
   
</div>
 </ContentTemplate>
              </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

