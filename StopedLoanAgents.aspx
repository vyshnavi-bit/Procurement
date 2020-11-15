<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StopedLoanAgents.aspx.cs" Inherits="StopedLoanAgents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        
        .style2
        {
            
            
             width: 60%;
            
            
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
<div style="background-color: #C0C0C0">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>


    

<asp:UpdateProgress ID="UpdateProgress1" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>



 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>




    <br />
    <table class="style1">
        <tr align="center">
            <td>
   <fieldset class="style2" >
   
   
       <table class="style1">
           <tr>
               <td align="left">
                   <asp:Label ID="Label1" runat="server" style="font-family: Andalus; color: #CC0000;" 
                       Text="From Date"></asp:Label>
                                <asp:TextBox ID="txt_FromDate" runat="server" 
                       CssClass="tb10" Font-Size="Small"  ></asp:TextBox>

                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" 
                       PopupPosition="TopRight">
                        </asp:CalendarExtender>
                              
                               </td>
               <td align="left">
                   <asp:Label ID="Label2" runat="server" style="font-family: Andalus; color: #CC0000;" 
                       Text="To Date"></asp:Label>
                              <asp:TextBox ID="txt_ToDate" runat="server" 
                       CssClass="tb10" Font-Size="Small"  ></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" 
                       PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                         
                          </td>
           </tr>
           <tr align="left">
               <td>
                   <asp:Label ID="Label3" runat="server" style="font-family: Andalus; color: #CC0000;" 
                       Text="Plant Code"></asp:Label>
                   <asp:DropDownList ID="ddl_PlantName" runat="server" CssClass="tb10" 
                       Font-Size="Small" Width="150px">
                   </asp:DropDownList>
               </td>
               <td align="left">
                   &nbsp;</td>
           </tr>
           <tr align =center>
               <td colspan="2">

                        <asp:Button ID="btn_ok" runat="server" BackColor="#FFFF99" BorderStyle="Double" 
                            Font-Bold="True" ForeColor="#333300" onclick="btn_ok_Click" Text="Get" 
                            CssClass="button2222" Height="30px" />
           <asp:Button ID="btn_print" runat="server" BackColor="#FFFF99" ForeColor="#333300" Text="Print" 
                            Height="30px" BorderStyle="Double" Font-Bold="True" 
                            OnClientClick = "return PrintPanel();" CssClass="button2222"  />

                        <asp:Button ID="btn_export" runat="server" BackColor="#FFFF99" BorderStyle="Double" 
                            Font-Bold="True" ForeColor="#333300" onclick="btn_export_Click" Text="Export" 
                            CssClass="button2222" Height="30px" />
                        <br />
               </td>
           </tr>

      
  

           
           <caption>
               <br />
           </caption>

      
  

           
           </table>
   
   
   </fieldset></td>
        </tr>
        </table>
                </fieldset>
                </td>
                </tr>
                </table>
&nbsp;</div>


   

     <asp:Panel id="pnlContents" runat = "server"> 

           <tr align="center">
               <td colspan="2">
               <center>
                   <br />
                   <asp:GridView ID="GridView1" runat="server" CssClass="gridview2" 
                       BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                       CellPadding="4" AutoGenerateColumns="False" Font-Size="Small" 
                       onrowcreated="GridView1_RowCreated" 
                       onrowdatabound="GridView1_RowDataBound" ShowFooter="true">
                       <Columns>
                        <asp:TemplateField HeaderText="SNo.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                           <asp:BoundField DataField="agent_Id" HeaderText="AgentId" 
                               SortExpression="agent_Id" />
                           <asp:BoundField DataField="loan_Id" HeaderText="LoanId" 
                               SortExpression="loan_Id" />
                           <asp:BoundField DataField="loanamount" HeaderText="LoanAmount" 
                               SortExpression="loanamount" />
                           <asp:BoundField DataField="balance" HeaderText="Balance" 
                               SortExpression="balance" />
                           <asp:BoundField DataField="loandate" HeaderText="loandate" 
                               SortExpression="loandate" />
                           <asp:BoundField DataField="Balance" HeaderText="LastRecoveryDate" 
                               SortExpression="Balance" />
                       </Columns>
                       <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                       <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                       <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                       <RowStyle BackColor="White" ForeColor="#330099" />
                       <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                       <SortedAscendingCellStyle BackColor="#FEFCEB" />
                       <SortedAscendingHeaderStyle BackColor="#AF0101" />
                       <SortedDescendingCellStyle BackColor="#F6F0C0" />
                       <SortedDescendingHeaderStyle BackColor="#7E0000" />
                   </asp:GridView>
                   <br />
                   </center>
               </td>
           </tr>

            </asp:Panel> 



          </ContentTemplate>
       
          <Triggers>
<asp:PostBackTrigger ControlID="btn_export" />
<asp:PostBackTrigger ControlID="btn_export" />

</Triggers>


 </asp:UpdatePanel>



       </table>




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
   </asp:Content>

