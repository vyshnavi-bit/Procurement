<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Rpt_CollectionCenterDifferenceLink.aspx.cs" Inherits="Rpt_CollectionCenterDifferenceLink" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
                  
    .buttonclass
{
padding-left: 10px;
font-weight: bold;
            height: 26px;
        }      
        
        .style1
    {
        height: 22px;
    }
    .style2
    {
        height: 37px;
    }
        
        </style>
   
    <script type="text/javascript">
        function Showalert() {
            alert('Call JavaScript function from codebehind');
        }
</script>

 <script type="text/javascript">

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
            <legend style="color: #3399FF">Vmcc DifferenceReport</legend>
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
                            <asp:Label ID="Label10" runat="server" CssClass="style7" Font-Size="Small" 
                                style="font-family: Andalus; font-size: medium" Text="From"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" 
                                Font-Size="X-Small" TabIndex="4" Width="145px"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="BottomRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                        </td>
                        <tr>
                            <td align="left" class="style2">
                            </td>
                            <td align="left" class="style2">
                                <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
                            </td>
                            <td align="left" class="style2">
                                <asp:TextBox ID="txt_ToDate" runat="server" CssClass="tb10" Font-Size="X-Small" TabIndex="5"  Width="145px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                                    PopupButtonID="txt_ToDate" PopupPosition="BottomRight" 
                                    TargetControlID="txt_ToDate">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                         <tr>
                            <td align="left">
                                &nbsp;</td>
                            <td align="left">
                                &nbsp;</td>
                            <td align="left">
                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="CheckBox1_CheckedChanged" Text="Click To AgentWise" />
                             </td>
                        </tr>
                        <tr>
                            <td align="left">
                                &nbsp;</td>
                            <td align="left">
                                <asp:Label ID="Label14" runat="server" CssClass="style7" Font-Size="Small" 
                                    style="font-family: Andalus; font-size: medium" Text="AgentId"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddl_agentid" runat="server" Font-Bold="true" 
                                    Font-Size="12px" Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
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
                                    onclick="btn_Export_Click" Text="Export" visible="true" />
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
         &nbsp;<asp:Panel id="pnlContents" runat = "server" bgcolor="White" >
   
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
                   <asp:GridView ID="grdLivepro" runat="server" AutoGenerateColumns="false" onrowdatabound="grdLivepro_RowDataBound" 
                onrowcreated="grdLivepro_RowCreated" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                Font-Size="14px" ShowFooter="true">    
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="White" ForeColor="#000066"  Font-Bold="True" Font-Size="16px"  />
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
                                            <asp:BoundField DataField="Agent_id" HeaderText="Name" ItemStyle-Width="40" />                                            
                                            <asp:TemplateField HeaderText="Aid" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkView" runat="server" CssClass="linkNoUnderline" 
                                                        ForeColor="Brown" OnClick="lnkView_Click" Text='<%# Eval("Agent_id") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                   
                                            <asp:BoundField DataField="CMilkkg" HeaderText="CMilkltr" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="CAfat" HeaderText="CAfat" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="CAsnf" HeaderText="CAsnf" ItemStyle-Width="20"/>                                
                                            <asp:BoundField DataField="Crate" HeaderText="Crate" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="CSamount" HeaderText="CSamount" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="pMilkkg" HeaderText="pMilkltr" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="PAfat" HeaderText="PAfat" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="PAsnf" HeaderText="PAsnf" ItemStyle-Width="20"/>                                
                                            <asp:BoundField DataField="PArate" HeaderText="PArate" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="pSamount" HeaderText="pSamount" ItemStyle-Width="20"/>  
                                            <asp:BoundField DataField="Diffmkg" HeaderText="Dmltr" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="Difffat" HeaderText="Dfat" ItemStyle-Width="20"/>
                                            <asp:BoundField DataField="Diffsnf" HeaderText="Dsnf" ItemStyle-Width="20"/>                                
                                            <asp:BoundField DataField="DiffRate" HeaderText="DRate" ItemStyle-Width="20"/>                                           
                                            <asp:BoundField DataField="DiffSamount" HeaderText="DSamount" ItemStyle-Width="20"/>                                   
                                       
                                        </Columns>
                                    </asp:GridView>  
        </td> 
        <tr>
        <td class="style1">
           <div align="center">
  <div>
      <asp:GridView ID="grdLivepro1" runat="server" 
          BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
          CellPadding="3" Font-Size="14px" onrowcreated="grdLivepro1_RowCreated" 
          onrowdatabound="grdLivepro1_RowDataBound" ShowFooter="True">
          <FooterStyle BackColor="White" ForeColor="#000066" />
          <HeaderStyle BackColor="White" ForeColor="#000066"  Font-Bold="True" Font-Size="16px"  />
          <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
         <RowStyle ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />    
          <Columns>
              <%--<asp:BoundField DataField="sno" HeaderText="Sno" ItemStyle-Width="20" />--%>
              <asp:TemplateField HeaderText="SNo">
                  <ItemTemplate>
                      <%#Container.DataItemIndex+1%>
                  </ItemTemplate>
                  <ItemStyle Width="2%" />
              </asp:TemplateField>
          </Columns>
      </asp:GridView>
  </div>
  </div></td>
        </tr>       
        </tr>
          
    
             
             
        </table>
        
    </fieldset>
   
    </asp:Panel>
    

        <asp:Panel id="pnlContentsvmcc" runat = "server" bgcolor="White" >
        <table>
         <tr valign="top" align="right">
         <td>
             <asp:Button ID="btn_Vmccprint" runat="server" BackColor="#006699" 
                 BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                 OnClientClick="return PrintPanelvmcc();" Text="Print" Visible=false  />
          <asp:Button ID="btn_VmccExport" runat="server" BackColor="#006699" 
                                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                                     Text="VmccExport" Visible="false" 
                 onclick="btn_VmccExport_Click" />             
         </td>
        </tr>
            <tr valign="top" align="center">
        <td>
            <asp:GridView ID="gv_Routewisemilk" runat="server" AutoGenerateColumns="false"                  
                onrowdatabound="gv_Routewisemilk_RowDataBound" 
                onrowcreated="gv_Routewisemilk_RowCreated"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                Font-Size="14px" ShowFooter="true" >
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="White" ForeColor="#000066"  Font-Bold="True" Font-Size="16px"  />
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
                                             <asp:BoundField DataField="producer_code" HeaderText="pro_code" />  
                                             <asp:BoundField DataField="prdate" HeaderText="prdate" />   
                                            <asp:BoundField DataField="sess" HeaderText="sess"  />                               
                                            <asp:BoundField DataField="CMilkltr" HeaderText="CMilkltr" />
                                            <asp:BoundField DataField="CAfat" HeaderText="CAfat" />
                                            <asp:BoundField DataField="CAsnf" HeaderText="CAsnf" />                                
                                            <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                           

                                        </Columns>
                                    </asp:GridView>


        </td>        
       
            </tr>
        </table>
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
<asp:PostBackTrigger ControlID="btn_Export" />
<asp:PostBackTrigger ControlID="btn_VmccExport" />
</Triggers>

        </asp:UpdatePanel>
</asp:Content>


