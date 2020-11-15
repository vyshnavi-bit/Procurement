<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_PlantwiseMilkmonitoring.aspx.cs" Inherits="Frm_PlantwiseMilkmonitoring" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
   <script type="text/javascript">
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
            <legend style="color: #3399FF">Milk Monitoring </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
            <td>
                &nbsp;</td>
            </tr>
             <tr>
                    <td>
                        &nbsp;</td>
                     <td align="right">
                       &nbsp;</td>
                    <td >
                  
                        <asp:CheckBox ID="chk_milk" runat="server" Checked="true" Font-Bold="true" 
                            Font-Size="14px" Text="Ince/Dec" />
                  
                    </td>
                    <td  align="left">
                                
                            </td>
                </tr>  
                  <tr>
                    <td>                                    
                    </td>
                     <td align="right">                   
                         &nbsp;</td>
                    <td  align="left">
                  
                        <asp:RadioButtonList ID="rdocheck" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="rdocheck_SelectedIndexChanged" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Sessions</asp:ListItem>
                            <asp:ListItem Value="2">PerDay</asp:ListItem>
                           <%-- <asp:ListItem Value="3">Period</asp:ListItem>--%>
                        </asp:RadioButtonList>
                  
                    </td>
                    <td  align="left">                    	
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
                  
                        <asp:DropDownList ID="ddl_PlantName" runat="server" Font-Bold="true"   Font-Size="12px"
                            onselectedindexchanged="ddl_PlantName_SelectedIndexChanged" Width="150px" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                      </td>
                    <td  align="left">                    	
                        &nbsp;</td>
                </tr> 
                 <tr>
                    <td>                                    
                    </td>
                     <td valign="top" align="right"> 
                         <asp:Label ID="lbl_Sess" runat="server" Text="Session"></asp:Label>
                     </td>
                    <td  align="left">               
                    <table>
                    <tr>
                    <td align="left">        
                        <asp:DropDownList ID="ddl_Sessions" runat="server" Width="60px">
                            <asp:ListItem Value="1">AM</asp:ListItem>
                            <asp:ListItem Value="2">PM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left">        
                        <asp:Label ID="lbl_CurrBilldate" runat="server" Text="CurrBill"></asp:Label>
                    </td>
                    <td align="left">        
                        <asp:DropDownList ID="ddl_Current" runat="server" Font-Bold="true" Font-Size="12px" >
                        </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                        <td align="left">
                        </td>
                        <td align="left">
                        </td>
                        <td align="left">
                        </td>
                        <tr>
                            <td align="left">
                            </td>
                            <td align="left">
                                <asp:Label ID="lbl_PreveBilldate" runat="server" Text="PrevBill"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddl_Previous" runat="server" Font-Bold="true" 
                                    Font-Size="12px" >
                                </asp:DropDownList>
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
                         <asp:Label ID="Label10" runat="server" CssClass="style7" Font-Size="Small" 
                             style="font-family: Andalus; font-size: medium" Text="From Date"></asp:Label>
                    </td>
                    <td >                         
                        <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" 
                            Font-Size="X-Small" TabIndex="4" Width="145px"></asp:TextBox>
                        <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                            Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="BottomRight" 
                            TargetControlID="txt_FromDate">
                        </asp:CalendarExtender>
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
                  
                        <asp:Button ID="btn_Generate" runat="server" BackColor="#006699" 
                            BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                            onclick="btn_Generate_Click" Text="Generate" />
                        <asp:Button ID="btn_print" runat="server" BackColor="#006699" 
                            BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                            OnClientClick="return PrintPanel();" Text="Print" />
                        <asp:Button ID="btn_Export" runat="server" BackColor="#006699" 
                            BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                            onclick="btn_Export_Click" Text="Export" />
                  
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
    <asp:Panel id="pnlContents" runat = "server" bgcolor="White" >
   
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
                   <asp:Label ID="Label12" runat="server" ForeColor="Red"
                       Font-Bold="true" Font-Size="Large"
                       Text="" CssClass="style11"></asp:Label>
                   <br />
                   <asp:Label ID="lbl_Reporttitle" runat="server" CssClass="style11" ForeColor="Green" 
                      Font-Bold="true" Font-Size="Large" Text=""></asp:Label>
                    </center> 
                   </span>
               </asp:Panel>
                    </center>
        </td>
        </tr>
        <tr valign="top" align="center">
        <td>
          <asp:GridView ID="grdLivepro" runat="server" AutoGenerateColumns="false" onrowdatabound="grdLivepro_RowDataBound" 
                onrowcreated="grdLivepro_RowCreated" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                Font-Size="12px" ShowFooter="true">    
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
                                            <%--<asp:BoundField DataField="sno" HeaderText="Sno" ItemStyle-Width="20" />--%>
                                           <asp:TemplateField HeaderText="SNo"> <ItemTemplate><%#Container.DataItemIndex+1%>                                                </ItemTemplate>
                                             <ItemStyle Width="2%" /> 
                                             </asp:TemplateField>
                                            <asp:BoundField DataField="Manager_Name" HeaderText="Manager" ItemStyle-Width="40" />
                                            
                                            <asp:TemplateField HeaderText="PlantName" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkView" runat="server" CssClass="linkNoUnderline" 
                                                        ForeColor="Brown" OnClick="lnkView_Click" Text='<%# Eval("Pname") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                           
                                            <asp:BoundField DataField="pMkg2" HeaderText="FrmDate" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="CMkg2" HeaderText="ToDate" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="Diff_Milkkg" HeaderText="Inc" ItemStyle-Width="20"/>                                
                                            <asp:BoundField DataField="Diff_Milkkg1" HeaderText="Dec" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="Iperc" HeaderText="Inc%" ItemStyle-Width="20"/>                                
                                            <asp:BoundField DataField="Dperc1" HeaderText="Dec%" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="Mana_PhoneNo" HeaderText="MPhoneNo" ItemStyle-Width="40"/>
                                            <asp:BoundField DataField="Plant_Code" HeaderText="" /> 
                                            <asp:BoundField DataField="Pname" HeaderText="" /> 
                                        </Columns>
                                    </asp:GridView>  
        </td> 
        <tr>
        <td>
           <div align="center">
  <asp:Label ID="Lbl_PlantName" runat="server" Font-Bold="true" Font-Size="Large"  ForeColor="Red" Text="PlantName :"></asp:Label>
  <asp:Label ID="lbl_Plantcode" runat="server" Font-Bold="true" Font-Size="Large" Font-Underline="true" ForeColor="Red"></asp:Label>
  <div>
  <asp:Label ID="lbl_Milkkg" runat="server" Visible=false Font-Bold="true" Font-Size="Large" Font-Underline="true" ForeColor="Green"></asp:Label>
  </div>
  </div></td>
        </tr>       
        </tr>
          
        <tr valign="top" align="center">
        <td>
          <asp:GridView ID="gv_Routewisemilk" runat="server" AutoGenerateColumns="false"                  
                onrowdatabound="gv_Routewisemilk_RowDataBound" 
                onrowcreated="gv_Routewisemilk_RowCreated"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                Font-Size="12px" ShowFooter="true" >
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
                                               <ItemTemplate><%#Container.DataItemIndex+1%> </ItemTemplate>
                                             <ItemStyle Width="2%" /> </asp:TemplateField>                                           
                                              <asp:BoundField DataField="SupervisorName" HeaderText="Supervisor" /> 
                                            <asp:TemplateField HeaderText="RouteName" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkView" runat="server" CssClass="linkNoUnderline" 
                                                        ForeColor="Brown" OnClick="lnkView1_Click" Text='<%# Eval("Route_Name") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>       
                                          
                                            <asp:BoundField DataField="pMkg2" HeaderText="FrmDate" ItemStyle-Width="20" />
                                            <asp:BoundField DataField="CMkg2" HeaderText="ToDate" ItemStyle-Width="20" />
                                             <asp:BoundField DataField="Diff_Milkkg1" HeaderText="Inc" ItemStyle-Width="20" />
                                            <asp:BoundField DataField="Diff_Milkkg" HeaderText="Dec" ItemStyle-Width="20" /> 
                                            <asp:BoundField DataField="Iperc" HeaderText="Inc%" ItemStyle-Width="20"/>                                
                                            <asp:BoundField DataField="Dperc1" HeaderText="Dec%" ItemStyle-Width="20"/>                                                                             
                                            <asp:BoundField DataField="Mobile" HeaderText="SPhoneNo" ItemStyle-Width="40" />                                            
                                            <asp:BoundField DataField="Rid" HeaderText="" />
                                            <asp:BoundField DataField="Plant_Code" HeaderText="" />
                                              <asp:BoundField DataField="Route_Name1" HeaderText="" />

                                        </Columns>
                                    </asp:GridView>


        </td>        
       
            </tr>
             <tr>
        <td>
          <div align="center">

   <asp:Label ID="Lbl_RouteName" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red" Text="RouteName :"></asp:Label>
  <asp:Label ID="Lbl_Routecode" runat="server" Font-Bold="true"  Font-Underline="true" Font-Size="Large" ForeColor="Red"></asp:Label>
    <div>
  <asp:Label ID="Lbl_Amilkkg" runat="server" Visible=false Font-Bold="true" Font-Size="Large" Font-Underline="true" ForeColor="Green"></asp:Label>
  </div></td>
        </tr>
              <tr valign="top" align="center">
        <td>
        <asp:GridView ID="Gv_AgentData" runat="server" ShowFooter="true" AutoGenerateColumns="false" onrowdatabound="Gv_AgentData_RowDataBound" 
                onrowcreated="Gv_AgentData_RowCreated" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Size="12px" >
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
                                               <ItemTemplate><%#Container.DataItemIndex+1%> </ItemTemplate>
                                             <ItemStyle Width="2%" /> </asp:TemplateField>                                    
                                              <asp:BoundField DataField="Agent_Name" HeaderText="AgentName" />   
                                              <asp:BoundField DataField="pMkg2" HeaderText="FrmDate" ItemStyle-Width="20" />
                                            <asp:BoundField DataField="CMkg2" HeaderText="ToDate" ItemStyle-Width="20" />
                                            <asp:BoundField DataField="Diff_Milkkg" HeaderText="Inc" ItemStyle-Width="20" /> 
                                            <asp:BoundField DataField="Diff_Milkkg1" HeaderText="Dec" ItemStyle-Width="20" /> 
                                            <asp:BoundField DataField="Iperc" HeaderText="Inc%" ItemStyle-Width="20"/>                                
                                            <asp:BoundField DataField="Dperc1" HeaderText="Dec%" ItemStyle-Width="20"/>                                                                                                                        
                                            <asp:BoundField DataField="phone_Number" HeaderText="SPhoneNo" ItemStyle-Width="40" />                                            
                                            
                                                                     
                                                                      
                                </Columns>
                            </asp:GridView> 
          
        </td>        
       
            </tr>
        </table>
        
    </fieldset>
   
    </asp:Panel>
    </div>
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
    
    </ContentTemplate>
         <Triggers>
<%--<asp:PostBackTrigger ControlID="Button3" />--%>
</Triggers>

        </asp:UpdatePanel>
         </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

