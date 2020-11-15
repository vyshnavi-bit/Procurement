<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgentMissingList.aspx.cs" Inherits="AgentMissingList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style5
        {
            font-family: Andalus;
        }
         .style6
        {
            width: 350px;
        }
        .style8
        {
            font-family: Andalus;
            color: #009900;
        }
        .style9
        {
            color: #FF0000;
        }
        .style10
        {
            color: #009900;
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
     
               <center>
              <center>
               <asp:Label ID="Label1" runat="server" Text="Agents Stopped Milk" 
                       style="font-family: Andalus; font-weight: 700;"></asp:Label>
             </center>
           <fieldset style="background-color: #CCFFFF"   class="style6">
           
           
                     <table class="style6">
                   <tr width=25%>
                       <td align=right width=25%>
                    
                   <asp:Label ID="Label22" runat="server" CssClass="style5" Text="Plant Code" 
                               Font-Bold="False"></asp:Label>
     
                       </td>
                       <td   align=left width=25%>
                           <strong>
                           <em>
                          <asp:DropDownList ID="ddl_Plantname" runat="server" 
                              onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" TabIndex="8" 
                              Width="190px" CssClass="tb10">
                          </asp:DropDownList>
                         
                       </td>
                   </tr>
                   <tr width=25%>
                       <td  align=right width=25%>
                     
                   <asp:Label ID="Label23" runat="server" CssClass="style5" Text="From Date" 
                               Font-Bold="False"></asp:Label>
     
                       </td>
                       <td  align=left width=25%>
                            <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" Height="25px" 
                               Width="185px"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender2" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                       </td>
                   </tr>
                   <tr width=25%>
                       <td  align=right width=25%>
                           
                           <asp:Label ID="Label24" runat="server" CssClass="style5" Font-Bold="False" 
                               Text="To Date"></asp:Label>
                          
                       </td>
                       <td  align=left width=25%>
                           <strong>
                           <asp:TextBox ID="txt_ToDate" runat="server" CssClass="tb10" Height="25px" 
                               Width="185px"></asp:TextBox>
                           <asp:CalendarExtender ID="txt_ToDate_CalendarExtender" runat="server" 
                               Format="dd/MM/yyyy" PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                               TargetControlID="txt_ToDate">
                           </asp:CalendarExtender>
                           <asp:CalendarExtender ID="txt_ToDate_CalendarExtender2" runat="server" 
                               Format="dd/MM/yyyy" PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                               TargetControlID="txt_ToDate">
                           </asp:CalendarExtender>
                          
                       </td>
                   </tr>
                   <tr width="25%">
                       <td align="right" width="25%">
                           <em>
                           <asp:DropDownList ID="ddl_Plantcode" runat="server" Enabled="False" 
                               Visible="False">
                           </asp:DropDownList>
                           </em>
                       </td>
                       <td align="left" width="25%">
                         
                           <asp:Button ID="btn_Save" runat="server" BackColor="#6F696F" 
                               CssClass="button2222" ForeColor="White" onclick="btn_Save_Click" TabIndex="10" 
                               Text="Show" Width="58px" Font-Size="Small" />
                           <asp:Button ID="btn_print" runat="server" BackColor="#6F696F" 
                               CssClass="button2222" ForeColor="White" onclick="btn_Save_Click" 
                               onclientclick="return PrintPanel();" TabIndex="10" Text="Print" 
                               Width="58px" Font-Size="Small" />
                           <asp:Button ID="Button2" runat="server" CssClass="button2222" 
                               onclick="Button2_Click" Text="Excel" Width="75px" Visible="False" />
                          
                       </td>
                   </tr>
               </table>
      
           </fieldset>
           <asp:Panel id="pnlContents" runat = "server">
           <center>
           <table class="style1">
           <center>
               <tr valign="top">
                   <td align="left" width="50%" colspan="3" >
                     <asp:Image ID="Image1"  runat="server" ImageUrl="~/Image/Pintvyslogo.png" 
                           Height="50px" ImageAlign="Middle" Width="100px"></asp:Image> 
                    
                   


               <center>    <asp:Label ID="lblpcode3" runat="server" 
                                   style="font-family: Andalus; color: #FF0000;" Text="Plant Name"></asp:Label>
                               <asp:Label ID="lblpcode" runat="server" CssClass="style10" 
                                   style="font-family: Andalus" Text="Label"></asp:Label>
                                   </center>
                   </td>

                  
                  
                       
                      
                  
               </tr>
               <tr valign="top">
                   <td align="right" width="50%">
                       <right>
                           <right>
                               <asp:Label ID="Label25" runat="server" 
                                   style="font-family: Andalus; color: #FF0000;" 
                                   Text="Milk Stopped Agents From:"></asp:Label>
                               <asp:Label ID="lblfrmdate" runat="server" CssClass="style8" Text="Label"></asp:Label>
                               <span class="style5"><span class="style9">To</span>:</span><asp:Label 
                                   ID="lbltodate" runat="server" CssClass="style8" Text="Label"></asp:Label>
                           </right>
                       </right>
                   </td>
                   <td align="right" width="2%">
                       &nbsp;</td>
                   <td align="left">
                       <asp:Label ID="Label26" runat="server" 
                           style="font-family: Andalus; color: #FF0000;" Text="Milk Missing Agent"></asp:Label>
                   </td>
               </tr>
               <tr valign="top">
                   <td align="right" width="48%" >
                       <asp:GridView ID="GridView1" runat="server" Font-Size="X-Small"  
                           HeaderStyle-ForeColor="orange" AllowPaging="True" AutoGenerateColumns="False" 
                           PageSize="20" onpageindexchanging="GridView1_PageIndexChanging">
                           <Columns>
                               <asp:BoundField DataField="Agent_id" HeaderText="Agent Id" 
                                   SortExpression="Agent_id" />
                               <asp:BoundField DataField="Agent_Name" HeaderText="Agent Name" 
                                   SortExpression="Agent_Name" />
                               <asp:BoundField DataField="Route_id" HeaderText="Route Id" 
                                   SortExpression="Route_id" />
                               <asp:BoundField DataField="Plant_code" HeaderText="Plant code" 
                                   SortExpression="Plant_code" />
                           </Columns>
<HeaderStyle ForeColor="Orange"></HeaderStyle>
                       </asp:GridView>
                   </td>
                   <td align="right" width="2%" >
                       &nbsp;</td>
                   <td align="left" width="48%">
                       <asp:GridView ID="GridView2" runat="server" Font-Size="X-Small"  
                           HeaderStyle-ForeColor="orange" AllowPaging="True" PageSize="20" 
                           AutoGenerateColumns="False">
                           <Columns>
                               <asp:BoundField DataField="AgentId" HeaderText="AgentId" 
                                   SortExpression="AgentId" />
                               <asp:BoundField DataField="pdate" HeaderText="Date" SortExpression="pdate" />
                               <asp:BoundField DataField="Smkg" HeaderText="SumOfMKg" SortExpression="Smkg" />
                               <asp:BoundField DataField="Afat" HeaderText="Fat" SortExpression="Afat" />
                               <asp:BoundField DataField="ASnf" HeaderText="Snf" SortExpression="ASnf" />
                           </Columns>
<HeaderStyle ForeColor="Orange"></HeaderStyle>
                       </asp:GridView>
                   </td>
               </tr>
               </center>
               <center>
               </center>
           </table>
           </center>
           <br />

      
       </center>
       </strong>
        </center>
    
    
    
   
    </asp:Panel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

