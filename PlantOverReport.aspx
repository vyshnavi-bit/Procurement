<%@ Page Title="Plant Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantOverReport.aspx.cs" Inherits="PlantOverReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
       
         .style6
        {
            width: 350px;
        }
        .style110
        {
           
           
            
              borderwidth:5px;
              color: firebrick;
             borderstyle:1;
         
          
         
                  font-size:12pt;
                   font-family: Andalus;
                 font-style:bold;
        }


          .style111
        {
              width:1px;
              color: navy;
              border-style: solid;
               border-width: 0px;
       
             borderstyle:1;
        
                  font-size:14pt;
                  font-family: Andalus;
                 font-style:bold;
                 text-align:left;
               
                  padding-left:5px; 
        }
        
            .style112
        {
              width:1px;
              color: navy;
              border-style: solid;
               border-width: 0px;
       
             borderstyle:1;
        
                  font-size:14pt;
                  font-family: Andalus;
                 font-style:bold;
               
               
                 
        }
        
        



        .style5
    {
        font-family: Andalus;
    }

        .style7
    {
        height: 30px;
    }

        .style8
    {
        height: 46px;
    }

        .newStyle1
    {
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


     <script type="text/javascript">
         //
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
         prm.add_beginRequest(BeginRequestHandler);
         // Raised after an asynchronous postback is finished and control has been returned to the browser.
         prm.add_endRequest(EndRequestHandler);
         function BeginRequestHandler(sender, args) {
             //Shows the modal popup - the update progress
             var popup = $find('<%= modalPopup.ClientID %>');
             if (popup != null) {
                 popup.show();
             }
         }

         function EndRequestHandler(sender, args) {
             //Hide the modal popup - the update progress
             var popup = $find('<%= modalPopup.ClientID %>');
             if (popup != null) {
                 popup.hide();
             }
         }       

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>


      <div >
<asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
   
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
      <left>    
          <asp:Image ID="imgUpdateProgress" align="left" runat="server" ImageUrl="waiting.gif" 
                AlternateText="Loading ..." ToolTip="Loading ..." 
                style="padding: 10px;position:fixed;top:47%; left:16%; bottom: 0px;" 
              ImageAlign="Left" />
                </left>  
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup"  />
</div>  


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     
               <center>
              <center>
               <asp:Label ID="Label1" runat="server" Text="PLANT REPORT" 
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
                              Width="190px" CssClass="tb10" AutoPostBack="True">
                          </asp:DropDownList>
                         
                       </td>
                   </tr>
                   <tr width=25%>
                       <td  align=right width=25% class="style8">
                     
                           <asp:Label ID="Label27" runat="server" CssClass="style5" Font-Bold="False" 
                               Text="Bill Dates"></asp:Label>
     
                       </td>
                       <td  align=left width=25% class="style8">
                            <strong><em>
                            <asp:DropDownList ID="ddl_Billdate" runat="server" CssClass="tb10" 
                                onselectedindexchanged="ddl_Billdate_SelectedIndexChanged" TabIndex="8" 
                                Width="190px" AutoPostBack="True">
                            </asp:DropDownList>
                            </em></strong>
                       </td>
                   </tr>
                   <tr width=25%>
                       <td  align=right width=25%>
                           
                           <asp:TextBox ID="TextBox1" Visible="false" runat="server"></asp:TextBox>
                       </td>
                       <td  align=left width=25%>
                           <asp:TextBox ID="TextBox2" Visible="false" runat="server"></asp:TextBox>
                       </td>
                   </tr>
                   <tr width="25%">
                       <td align="right" width="25%">
                           <asp:DropDownList ID="ddl_gettdate" runat="server" AutoPostBack="true" 
                               Height="16px" Visible="false" Width="29px">
                           </asp:DropDownList>
                           <asp:DropDownList ID="ddl_getfdate" runat="server" AutoPostBack="true" 
                               Height="16px" Visible="false" Width="29px">
                           </asp:DropDownList>
                           <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                               Height="16px" Visible="false" Width="29px">
                           </asp:DropDownList>
                       </td>
                       <td align="left" width="25%">
                         
                           
                          
                           <asp:Button ID="btn_print" runat="server" CssClass="button2222" 
                               onclick="btn_Save_Click" Text="Print" Width="58px" BackColor="#6F696F" 
                               Font-Size="Small" ForeColor="White" onclientclick="return PrintPanel();" 
                               TabIndex="10" />
                         

                         <asp:Button ID="Button1" runat="server" Text="Show" onclick="Button1_Click" 
                               CssClass="button2222" />

                                                    

                         <asp:Button ID="Button2" runat="server" Text="Excel" 
    onclick="Button2_Click" CssClass="button2222" Visible="False" />

                           
                          
                       
                          
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
                   <td align="right" width="50%" class="style7" colspan="3">
                   <center>
                       <asp:GridView ID="GridView1" runat="server" Font-Size="Small"   
                           HeaderStyle-ForeColor="orange" 
                           AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" 
                           BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="375px" 
                           onselectedindexchanged="GridView1_SelectedIndexChanged" CellSpacing="2" 
                           CssClass="gridview1">
                           
                           <Columns>
                          <asp:TemplateField  HeaderText="Plant Bill Period Status">
                            <ItemTemplate>
                             <itemstyle width="20%"  horizontalalign="right">
                            
<asp:Table runat="server"></asp:Table>

                  <asp:Label class="style112" runat="server" Text="FromDate:"></asp:Label>                   
                      <asp:Label ID="label55" class="style110" runat="server" Text='<%# Eval("Frm_date")%>'></asp:label>
                      <asp:Label class="style112"  runat="server" Text="ToDate:"></asp:Label>
                      <asp:Label ID="Label156" class="style110" runat="server"  Text='<%# Eval("To_date")%>'></asp:Label><br /> 
                      ________________________________________________________
                     <asp:Label  class="style111" width="140px" runat="server"  Text="MilkKg:"></asp:Label></asp:Label>
                     <asp:Label ID="Label1"  class="style110"  runat="server" Text='<%# Eval("TotMkg") %>'></asp:Label><br /> 
                     ________________________________________________________
                    <asp:Label class="style111" width="140px" runat="server" Text="Milk Ltr:"></asp:Label>
                     <asp:Label ID="Label2" class="style110" runat="server" Text='<%# Eval("TotMltr") %>'></asp:Label><br />
                     ________________________________________________________
                     <asp:Label class="style111" width="140px" runat="server" Text="PerDayLtr   :"></asp:Label>
                     <asp:Label ID="Label3" class="style110" runat="server" Text='<%# Eval("PerDayLtr") %>'></asp:Label><br />
                     ________________________________________________________
                     <asp:Label class="style111" width="140px" runat="server" Text="Ltrcost     :"></asp:Label>
                     <asp:Label ID="Label4" class="style110"  runat="server" Text='<%# Eval("Ltrcost") %>'></asp:Label><br />
                     ________________________________________________________
                     <asp:Label class="style111" width="140px" runat="server" Text="Kgfat       :"></asp:Label>
                     <asp:Label ID="Label5" class="style110" runat="server"  Text='<%# Eval("Kgfat") %>'></asp:Label><br />
                     ________________________________________________________
                     <asp:Label class="style111" width="140px" runat="server" Text="Avgfat      :"></asp:Label>
                     <asp:Label ID="Label6" class="style110" runat="server" Text='<%# Eval("Avgfat") %>'></asp:Label><br />
                     ________________________________________________________
                     <asp:Label class="style111" width="140px" runat="server" Text="Kgsnf       :"></asp:Label>
                     <asp:Label ID="Label7" class="style110" runat="server" Text='<%# Eval("Kgsnf") %>'></asp:Label><br />
                     ________________________________________________________
                     <asp:Label class="style111" width="140px"  runat="server" Text="Avgsnf     :"></asp:Label>
                     <asp:Label ID="Label8" class="style110" width="150px" runat="server" Text='<%# Eval("Avgsnf") %>'></asp:Label><br />
                     ________________________________________________________
                      <asp:Label ID="Label1012" width="140px" class="style111" runat="server" Text="Ts:"></asp:Label>
                     <asp:Label ID="Label1113" class="style110" runat="server" Text='<%# Eval("Ts") %>'></asp:Label><br />
                     ________________________________________________________

                     <asp:Label class="style111" width="140px" runat="server" Text="TotalAmount:"></asp:Label>
                     <asp:Label ID="Label9" class="style110" runat="server" Text='<%# Eval("TotalAmount") %>'></asp:Label><br />

                     
              

                       ________________________________________________________

                     <asp:Label class="style111" width="140px" runat="server" Text="Bill Advance:"></asp:Label>
                     <asp:Label ID="Label70" class="style110" runat="server" Text='<%# Eval("Billadv") %>'></asp:Label><br />

                     
                     




                       ________________________________________________________

                     <asp:Label class="style111" width="140px" runat="server" Text="Medicine:"></asp:Label>
                     <asp:Label ID="Label71" class="style110" runat="server" Text='<%# Eval("Ai") %>'></asp:Label><br />

                     
                     





                       ________________________________________________________

                     <asp:Label class="style111" width="140px" runat="server" Text="Feed:"></asp:Label>
                     <asp:Label ID="Label72" class="style110" runat="server" Text='<%# Eval("Feed") %>'></asp:Label><br />

                     
                     





                       ________________________________________________________

                     <asp:Label class="style111" width="140px" runat="server" Text="Can:"></asp:Label>
                     <asp:Label ID="Label73" class="style110" runat="server" Text='<%# Eval("can") %>'></asp:Label><br />

                     
                     




                       ________________________________________________________

                     <asp:Label class="style111" width="140px" runat="server" Text="Recovery:"></asp:Label>
                     <asp:Label ID="Label74" class="style110" runat="server" Text='<%# Eval("recovery") %>'></asp:Label><br />

                     
                     ________________________________________________________


                       <asp:Label class="style111" width="140px" runat="server" Text="others:"></asp:Label>
                     <asp:Label ID="Label75" class="style110" runat="server"  Text='<%# Eval("others") %>'></asp:Label><br />
                     ________________________________________________________


                         

                       <asp:Label class="style111" width="140px" runat="server" Text="Loan Deduction:"></asp:Label>
                     <asp:Label ID="Label76" class="style110" runat="server"  Text='<%# Eval("Sinstamt") %>'></asp:Label><br />
                     



                     ________________________________________________________


                       <asp:Label class="style111" width="140px" runat="server" Text="ClaimAount:"></asp:Label>
                     <asp:Label ID="Label77" class="style110" runat="server"  Text='<%# Eval("ClaimAount") %>'></asp:Label><br />
                     ________________________________________________________


                       


                       <asp:Label class="style111" width="140px" runat="server" Text="Roundoff:"></asp:Label>
                     <asp:Label ID="Label78" class="style110" runat="server"  Text='<%# Eval("roundoff") %>'></asp:Label><br />
                     ________________________________________________________
                     
                       <asp:Label class="style111" width="140px" runat="server" Text="Net Bill Amount:"></asp:Label> 
                     <asp:Label ID="Label912" class="style110" runat="server" Text='<%# Eval("Netpay") %>'></asp:Label><br />
                      ________________________________________________________


                                   <br />
                                                             
                                    </itemstyle>
                                </ItemTemplate>
                               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                               <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                           </Columns>
                           <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                           <HeaderStyle ForeColor="White" BackColor="#A55129" Font-Bold="True" />
                           <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                           <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                           <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                           <SortedAscendingCellStyle BackColor="#FFF1D4" />
                           <SortedAscendingHeaderStyle BackColor="#B95C30" />
                           <SortedDescendingCellStyle BackColor="#F1E5CE" />
                           <SortedDescendingHeaderStyle BackColor="#93451F" />
                       </asp:GridView >
                       </center>
                   </td>
               </tr>
               <tr valign="top">
                   <td align="right" class="style7" width="50%">
                       &nbsp;</td>
                   <td align="right" class="style7" width="2%">
                   </td>
                   <td align="left" class="style7">
                       &nbsp;</td>
               </tr>
               <tr valign="top">
                   <td align="right" width="48%" >
                       &nbsp;</td>
                   <td align="right" width="2%" >
                       &nbsp;</td>
                   <td align="left" width="48%">
                       &nbsp;</td>
               </tr>
               </center>
               <center>
               </center>
           </table>
           </center>
            </asp:Panel>
           <br />

      
       </center>
       </strong>
        </center>
    
    
    
   
   
     </ContentTemplate>
 </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

