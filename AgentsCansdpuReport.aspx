<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgentsCansdpuReport.aspx.cs" Inherits="AgentsCansdpuReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

   <style type="text/css">
        .style1
        {
            font-family: Andalus;
            font-size: medium;
        }
                
         .style3
        {
            width: 65%;
        }
        
        
        
        
        .buttonclass
{
padding-left: 10px;
font-weight: bold;
}
.buttonclass:hover
{
color: white;
background-color:Orange;
}


.columnscss
{
width:25px;
font-weight:bold;
font-family:Verdana;
}

        
        
       .style3
       {
           height: 36px;
       }

        .style3
       {
           height: 36px;
       }
        
        
       .style4
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



 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
          EnablePageMethods="true">
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
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
















      <center>
      <panel width=70%>
      <center>
      <fieldset width="90%" bgcolor="#CCFFFF">
      
      
          <table width=100%>
              <tr>
                  <td width=20% align="left">
                    
                      <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" 
                          PostBackUrl="~/Agentscandetails.aspx">Back</asp:LinkButton>
                    
                  </td>
                  <td  width=20% style="width: 40%">
               <center>     <asp:Label ID="Label18" runat="server" CssClass="style4" EnableTheming="False" 
                        Font-Size="Small" 
                        style="font-family: Andalus; font-size: medium; font-weight: 700;" 
                        Text="Agent Information "></asp:Label> 
                        </center>
                        
                        </td>
                  <td  width=20%>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td colspan="3" class="style9">
                      <asp:Panel ID="Panel9" runat="server" BorderStyle="Inset">
                      <br />

                 <center>         <table width="50%" style="background-color: #CCFFFF" >
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label43" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Plant Name"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                                          class="ddl2" CssClass="ddl2" Height="30px" 
                                          onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="230px">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="True" 
                                          CssClass="tb10" Font-Size="X-Small" Height="20px" 
                                          onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Visible="False" 
                                          Width="70px">
                                      </asp:DropDownList>
                                  </td>
                                  <td align="center">
                                      <asp:Button ID="Button1" runat="server" CssClass="buttonclass" 
                                          onclick="Button1_Click" OnClientClick="return validate();" Text="Submit" />
                                      <asp:Button ID="Button2" runat="server" BorderColor="#663300" 
                                          BorderStyle="Inset" BorderWidth="1px" CssClass="buttonclass" Font-Bold="True" 
                                          Height="23px" OnClientClick="return PrintPanel();" Text="Print" />
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      &nbsp;</td>
                                  <td align="left">
                                      <asp:RadioButton ID="Rtoall" runat="server" AutoPostBack="True" Checked="True" 
                                          CssClass="style1" oncheckedchanged="Rtoall_CheckedChanged" Text="All Agents" />
                                      <asp:RadioButton ID="rdosingle" runat="server" AutoPostBack="True" 
                                          CssClass="style1" oncheckedchanged="rdosingle_CheckedChanged" 
                                          Text="Single Agents" />
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      <asp:Label ID="Label21" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Agent Id/Can No"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:DropDownList ID="ddl_Agentid" runat="server" AutoPostBack="True" 
                                          class="ddl2" CssClass="ddl2" Height="30px" 
                                          onselectedindexchanged="ddl_Agentid_SelectedIndexChanged" Width="230px">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                          </table> </center>

                      <br />

                      </asp:Panel>
                  </td>
              </tr>
              </table>
      

      </fieldset>
      </center>
          <br />
      <div>
      </panel>
         <asp:Panel id="pnlContents" runat = "server"> 
        
                        <br />
                        <asp:Label ID="lbl_pname" runat="server" 
                            style="font-family: Andalus; color: #CC3300; font-weight: 700;"></asp:Label>
                        <br />
        
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            CssClass="GridViewStyle1" HeaderStyle-BackColor="#61A6F8" ShowFooter="True"
                            PageSize="5" Font-Size="15px" 
              onpageindexchanging="GridView1_PageIndexChanging" 
                            onrowdatabound="GridView1_RowDataBound">
                            <Columns>
                     
                                <asp:BoundField DataField="Agent_Id" HeaderText="AgentId"  HeaderStyle-ForeColor="brown"
                                    SortExpression="Agent_Id" >
                                <HeaderStyle ForeColor="Brown" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DateIssuingOrReceiving" HeaderText="Date"   HeaderStyle-ForeColor="brown"
                                    SortExpression="DateIssuingOrReceiving" >
                                <HeaderStyle ForeColor="Brown" />
                                </asp:BoundField>



                            

                                   
                                  <asp:TemplateField HeaderText="IssuedCans" HeaderStyle-ForeColor="brown">
		                      	 <ItemTemplate>
			                  	<div style="text-align: right;">
			                   	<asp:Label ID="lblcanissue" runat="server" Text='<%# Eval("CanIssuing") %>' />
			                 	</div>
			                 </ItemTemplate>
			                <FooterTemplate>
			            	<div style="text-align: right;">
			             	<%--<asp:Label ID="lblTotalcanissue" runat="server"     Text="Total Cans"   HeaderStyle-ForeColor="brown" />--%>
                           <asp:Label ID="rrr"  runat="server" Text="TotalCans"  ForeColor="brown" Font-Bold="true"></asp:Label> 
                            <asp:Label ID="lblTotalcanissue" runat="server" Font-Bold="true"  ForeColor="Green"></asp:Label>
                            <br />
                            <br />
                              <asp:Label ID="Label2"  runat="server" Text="PendingCans"  ForeColor="Red" Font-Bold="true"></asp:Label> 
                            <asp:Label ID="Lblpending" runat="server" Font-Bold="true"  ForeColor="Green"></asp:Label>
			              	</div>
			                    </FooterTemplate>
		                              <HeaderStyle ForeColor="Brown" />
		                       </asp:TemplateField>












                                   
                                   <asp:TemplateField HeaderText="ReceiveCans" HeaderStyle-ForeColor="brown"  SortExpression="CanIssuing">
		                      	 <ItemTemplate>
			                  	<div style="text-align: right;">
			                   	<asp:Label ID="lblCanReceiving" runat="server" Text='<%# Eval("CanReceiving") %>' />
			                 	</div>
			                 </ItemTemplate>
			                <FooterTemplate>
			            	<div style="text-align: right;">
			            <%-- 	<asp:Label ID="lblTotalCanReceiving" runat="server"   Text="Total Cans"/>--%>
                                <asp:Label ID="rrr1"  runat="server" Text="TotalCans"  ForeColor="brown" Font-Bold="true"></asp:Label> 
                            <asp:Label ID="lblTotalCanReceiving" runat="server"  Font-Bold="true" ForeColor="Green"></asp:Label>
                                 <br />                
                                 <br />
                                 <br />                
                                                  <%-- visible false--%>
                            <%--<br />
                            <br />
                              <asp:Label ID="Label13222"  runat="server" Text="DpuPendingCans" Visible="false"  ForeColor="Magenta" Font-Bold="true"></asp:Label> 
                            <asp:Label ID="LblDpupending" runat="server" Font-Bold="true" Visible="false" ForeColor="Green"></asp:Label>--%>
                                                       
                            <%-- <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Green">Grand Total:</asp:Label><br />--%>
			              	</div>
			                    </FooterTemplate>
		                               <HeaderStyle     ForeColor="Brown" />
		                       </asp:TemplateField>



                                    
                                 




                                    
                                <asp:BoundField DataField="CanType" HeaderText="CanType"   HeaderStyle-ForeColor="brown"
                                    SortExpression="CanType" >
                                <HeaderStyle ForeColor="Brown" />
                                </asp:BoundField>



                            

                                   



                                    
                                  <asp:TemplateField HeaderText="IssuedDpu" HeaderStyle-ForeColor="brown" 
                                    SortExpression="DpuIssuing">
		                      	 <ItemTemplate>
			                  	<div style="text-align: right;">
			                   	<asp:Label ID="lbldpuissue" runat="server" Text='<%# Eval("DpuIssuing") %>' />
			                 	</div>
			                 </ItemTemplate>
			                <FooterTemplate>
			            	<div style="text-align: right;">
			             	<%--<asp:Label ID="lblTotalcanissue" runat="server"     Text="Total Cans"   HeaderStyle-ForeColor="brown" />--%>
                           <asp:Label ID="rrr21"  runat="server" Text="TotalDpuSys"  ForeColor="brown" Font-Bold="true"></asp:Label> 
                            <asp:Label ID="lblTotalDpuissue" runat="server"  Font-Bold="true" ForeColor="Green"></asp:Label> 
                            <br />
                            <br />
                              <asp:Label ID="Label13222"  runat="server" Text="DpuPendingCans"  ForeColor="Red" Font-Bold="true"></asp:Label> 
                            <asp:Label ID="LblDpupending" runat="server" Font-Bold="true"  ForeColor="Green"></asp:Label>

			              	</div>
			                    </FooterTemplate>
		                              <HeaderStyle ForeColor="Brown" />
		                       </asp:TemplateField>




                                    <asp:TemplateField HeaderText="ReceivedDpu"            HeaderStyle-ForeColor="brown" SortExpression="DpuReceiving">
		                      	 <ItemTemplate>
			                  	<div style="text-align: right;">
			                   	<asp:Label ID="lbldpurec" runat="server" Text='<%# Eval("DpuReceiving") %>' />
			                 	</div>
			                 </ItemTemplate>
			                <FooterTemplate>
			            	<div style="text-align: right;">
			             	<%--<asp:Label ID="lblTotalcanissue" runat="server"     Text="Total Cans"   HeaderStyle-ForeColor="brown" />--%>
                           <asp:Label ID="rrr11"  runat="server" Text="TotalDpuSys"  ForeColor="brown" Font-Bold="true"></asp:Label> 
                            <asp:Label ID="lblTotalDpurec" runat="server"  Font-Bold="true" ForeColor="Green"></asp:Label>

                         <%--   visible false--%>
                          <br />
                            <br />
                            <br />
                           <%--   <asp:Label ID="Label13222"  runat="server" Text="DpuPendingCans" Visible="false"  ForeColor="brown" Font-Bold="true"></asp:Label> 
                            <asp:Label ID="LblDpupending" runat="server" Font-Bold="true" Visible="false" ForeColor="Green"></asp:Label>--%>

			              	</div>
			                    </FooterTemplate>
		                              <HeaderStyle ForeColor="Brown" />
		                       </asp:TemplateField>

                               


                                    




                                    
                                  <asp:BoundField DataField="DpuType" HeaderText="DpuType"  HeaderStyle-ForeColor="brown"
                                    SortExpression="DpuType" >



                                    
                                 




                                    
                                  <HeaderStyle ForeColor="Brown" />
                                </asp:BoundField>



                                    
                                 




                                    
                            </Columns>

<HeaderStyle BackColor="#61A6F8"></HeaderStyle>
                            </asp:GridView>
                               </asp:panel>
                  </div>
     </center>
     <br />

        </ContentTemplate>
        </asp:UpdatePanel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

