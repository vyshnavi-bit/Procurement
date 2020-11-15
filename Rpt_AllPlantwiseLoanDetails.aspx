<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Rpt_AllPlantwiseLoanDetails.aspx.cs" Inherits="Rpt_AllPlantwiseLoanDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">

    function PrintPanelLoanId() {
        var panel = document.getElementById("<%=pnlContentsLoanId.ClientID %>");
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

    function PrintPanelvmcc() {
        var panel = document.getElementById("<%=pnlContentsvmcc.ClientID %>");
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
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server" />

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
               <div class="legagentsms">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">All Plant Loan Report</legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
            <td>
                &nbsp;</td>
            </tr>
             
                 
                 <tr>
                    <td>                                    
                    </td>
                     <td align="left">                   
                         <asp:Label ID="Label6" runat="server" CssClass="style7" Font-Size="Small" 
                             style="font-family: Andalus; font-size: medium"  Text="PlantName"></asp:Label>
                      </td>
                    <td  align="left">
                  
                        <asp:DropDownList ID="ddl_PlantName" runat="server" Font-Bold="true"   Font-Size="12px"  Width="150px">
                        </asp:DropDownList>
                      </td>
                    <td  align="left">                    	
                        &nbsp;</td>
                </tr> 
                 
                    <tr>
                        <td align="left">
                        </td>
                        <td align="left">
                            <asp:Label ID="Label10" runat="server" CssClass="style7" Font-Size="Small" Visible="false"
                                style="font-family: Andalus; font-size: medium" Text="From"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" Visible="false"
                                Font-Size="X-Small" TabIndex="4" Width="10"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="BottomRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                            <asp:Label ID="Label3" runat="server" Text="To" Visible="false"></asp:Label>
                            <asp:TextBox ID="txt_ToDate" runat="server" CssClass="tb10" Font-Size="X-Small" 
                                TabIndex="5" Visible="false" Width="10px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                                PopupButtonID="txt_ToDate" PopupPosition="BottomRight" 
                                TargetControlID="txt_ToDate">
                            </asp:CalendarExtender>
                        </td>
                        
                         <tr>
                            <td align="left">
                            </td>
                            <td align="left">
                            </td>
                            <td align="left">
                                <asp:Button ID="btn_Generate" runat="server" BackColor="#006699" 
                                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                                    onclick="btn_Generate_Click" Text="Generate" />
                                <asp:Button ID="btn_print" runat="server" BackColor="#006699" 
                                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                                    OnClientClick="return PrintPanel();" Text="Print" />
                                <asp:Button ID="btn_Export" runat="server" BackColor="#006699" 
                                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                                    onclick="btn_Export_Click" Text="Export" visible="true"/>
                             </td>
                        </tr>
                    </tr>
                    </table>
                    </td>
                    <td  align="left"> 
                                            	
                    	&nbsp;</td>
                </tr> 
                 
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="right">         
                    </td>
                    <td >                         
                    </td>
                    <td  align="left">
                        &nbsp;</td>
                               <td width="12%">
                                 
                    </td>
                </tr>   
                 <tr>
                    <td>                                    
                    </td>
                     <td>                   
         
                         &nbsp;</td>
                    <td  align="left">
                  
                    </td>
                    <td  align="left">                    	
                        <asp:Label ID="Label13" runat="server" CssClass="style11"  visible="false" 
                       style="font-weight: 700; font-size: small" Text="Plant Name"></asp:Label>
                   <asp:Label ID="lblpname" runat="server" visible="false"  
                       style="font-weight: 700; text-align: left;" Text="Label" 
                       Width="42px" CssClass="style11"></asp:Label></td>
                </tr>  
                     
                
            </table>
            <br />
          
   </fieldset>
   </div>
     <div  ><asp:Label ID="Lbl_Errormsg" runat="server" ForeColor="Red"  ></asp:Label></div>

     <div align="center">
      <asp:Panel id="pnlContents" runat = "server" >
   
    <fieldset width="100%" >       
        <table >
        <tr>
        <td>
         <center>         
               <asp:Panel ID="Panel1" HorizontalAlign="Center" runat="server"  >
                   <span class="style9">
           <center>        <asp:Image ID="Image1" runat="server" Height="50px" 
                       ImageUrl="~/Image/VLogo.png" Width="100px" /> 
                   <br />
                   <br />
                    </center> 
                   </span>
               </asp:Panel>
                    </center>
        </td>
        </tr>
        <tr valign="top" align="center">
        <td>
                   <asp:GridView ID="AgentwiseLoan" runat="server" AutoGenerateColumns="false"  
                  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                Font-Size="12px" ShowFooter="true" 
                       onrowdatabound="AgentwiseLoan_RowDataBound">    
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
                                           <asp:TemplateField HeaderText="SNo"> <ItemTemplate><%#Container.DataItemIndex+1%>                                                </ItemTemplate>
                                             <ItemStyle Width="2%" /> 
                                             </asp:TemplateField>                                                                                        
                                            <asp:TemplateField HeaderText="Agent_Name" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkView" runat="server" CssClass="linkNoUnderline" 
                                                        ForeColor="Brown" OnClick="lnkView_Click" Text='<%# Eval("Agent_Name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                           
                                            <asp:BoundField DataField="LAmount" HeaderText="LAmount" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="LrecoveryAmount" HeaderText="LrecoveryAmount" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="Lbalance" HeaderText="Lbalance" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="agent_Id" HeaderText="" />                                
                                                                             
                                       
                                        </Columns>
                                    </asp:GridView>  
        </td>             
        </tr>     
             
        </table>
        
    </fieldset>
   
    </asp:Panel>
     </div>    

    <div align="center">

        <asp:Panel id="pnlContentsvmcc" runat = "server" bgcolor="White" >
        <table>
         <tr valign="top" align="right">
         <td>
             <asp:Button ID="btn_AgentLoanIdwiseprint" runat="server" BackColor="#006699" 
                 BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                 OnClientClick="return PrintPanelvmcc();" Text="Print" Visible="false"  />
          <asp:Button ID="btn_AgentLoanIdwiseExport" runat="server" BackColor="#006699" 
                                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                                     Text="Export" Visible="false" 
                 onclick="btn_AgentLoanIdwiseExport_Click"  />             
         </td>
        </tr>
        <tr valign="top" align="center">
        <td>
               <asp:GridView ID="AgentLoanIdwise" runat="server" AutoGenerateColumns="false"  
                  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                   BorderWidth="1px" CellPadding="3" 
                                Font-Size="12px" ShowFooter="true" 
                   onrowdatabound="AgentLoanIdwise_RowDataBound">    
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
                                           <asp:TemplateField HeaderText="SNo"> <ItemTemplate><%#Container.DataItemIndex+1%>                                                </ItemTemplate>
                                             <ItemStyle Width="2%" /> 
                                             </asp:TemplateField>                                                                                        
                                            <asp:TemplateField HeaderText="loan_Id" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkView1" runat="server" CssClass="linkNoUnderline" 
                                                        ForeColor="Brown" OnClick="lnkView1_Click" Text='<%# Eval("loan_Id") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                            <asp:BoundField DataField="EntryDate" HeaderText="EntryDate" ItemStyle-Width="20"/>                                         
                                            <asp:BoundField DataField="LAmount" HeaderText="LAmount" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="LrecoveryAmount" HeaderText="LrecoveryAmount" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="Lbalance" HeaderText="Lbalance" ItemStyle-Width="20"/>                                
                                             <asp:BoundField DataField="loan_Id" HeaderText="" />                                
                                                                            
                                        </Columns>
                                    </asp:GridView>  

        </td>        
       
         </tr>
        </table>
        </asp:Panel>
        </div>

       <div align="center">

        <asp:Panel id="pnlContentsLoanId" runat = "server" bgcolor="White" >
        <table>
         <tr valign="top" align="right">
         <td>
             <asp:Button ID="btn_LoanIdprint" runat="server" BackColor="#006699" 
                 BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                 OnClientClick="return PrintPanelLoanId();" Text="Print" Visible="false"  />
          <asp:Button ID="btn_LoanIdExport" runat="server" BackColor="#006699" 
                                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                                     Text="Export" Visible="false" 
                 onclick="btn_LoanIdExport_Click" />             
         </td>
        </tr>
        <tr valign="top" align="center">
        <td>
               <asp:GridView ID="LoanId" runat="server" AutoGenerateColumns="false"  
                  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                   BorderWidth="1px" CellPadding="3" 
                                Font-Size="12px" ShowFooter="true" 
                   onrowdatabound="LoanId_RowDataBound">    
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
                                           <asp:TemplateField HeaderText="SNo"> <ItemTemplate><%#Container.DataItemIndex+1%>                                                </ItemTemplate>
                                             <ItemStyle Width="2%" /> 
                                             </asp:TemplateField>                                                                                        
                                             <asp:BoundField DataField="Paid_date" HeaderText="Paid_date" ItemStyle-Width="20"/>                                       
                                            <asp:BoundField DataField="Openningbalance" HeaderText="Openbalance" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="Paid_Amount" HeaderText="Paid_Amount" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="Closingbalance" HeaderText="Closebalance" ItemStyle-Width="20"/>                                
                                                                             
                                       
                                        </Columns>
                                    </asp:GridView>  

        </td>        
       
         </tr>
        </table>
        </asp:Panel>
        </div>


             </ContentTemplate>
         <Triggers>
<asp:PostBackTrigger ControlID="btn_Export" />

</Triggers>

        </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

