<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Agentscandetails.aspx.cs" Inherits="Agentscandetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">



<style type="text/css">
                
        
        
        
        
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

        
        
    
        .style4
        {
            color: #333300;
            }


        
        
 
        
        
        
        
        

         .ddl
        {
            border:2px solid #7d6754;
            border-radius:5px;
            padding:3px;
            -webkit-appearance: none; 
            background-image:url('Images/Arrowhead-Down-01.png');
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            text-overflow: '';/*In Firefox*/
        }
        
        
        
        
 
        
        
        
        
        

        .style1
        {
        font-family: Andalus;
        font-size: medium;
        font-weight: 700;
        color: #CC00CC;
    }
                
        
        
        
 
        
        
        
        
        

        .style5
    {
        font-family: Andalus;
        font-weight: 700;
        color: #CC00CC;
    }
                
        
        .style15
    {
        width:65%;
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
      
      <fieldset width="90%" bgcolor="#CCFFFF">
      
      
          <table width=100%>
              <tr>
                  <td align="left" width="20%">
                      &nbsp;</td>
                  <td style="width: 40%" width="20%">
                      <asp:Label ID="Label18" runat="server" CssClass="style4" EnableTheming="False" 
                          Font-Size="Small" 
                          style="font-family: Andalus; font-size: medium; font-weight: 700;" 
                          Text="Agent Can Distributing/Receving"></asp:Label>
                  </td>
                  <td width="20%">
                      <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                          EnableViewState="False" onclick="LinkButton1_Click" 
                          PostBackUrl="~/AgentsCansdpuReport.aspx" style="font-family: Andalus">Show Reports</asp:LinkButton>
                  </td>
              </tr>
              <tr>
                  <td colspan="3" class="style9">
                      <asp:Panel ID="Panel9" runat="server" BorderStyle="Inset">
                      <br />
            <center>          <fieldset  class="style15" style="background-color: #CCFFFF">
              <center> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                  <asp:Label ID="Label56" runat="server" 
                      style="font-family: Andalus; font-weight: 700; color: #CC0000;"      
                      Text="Can Issuing And Receiving"></asp:Label>
                  <br />
                </center> 

                      <center>   
                                               <table>
                              <tr>
                                  <td width=25% align="right">
                                      &nbsp;</td>
                                  <td width=30% align="left">
                                      <asp:Panel ID="Panel10" runat="server" BorderStyle="Inset" Width="225px">
                                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                          <asp:RadioButton ID="Rdoissue" runat="server" AutoPostBack="True" 
                                              CssClass="style1" oncheckedchanged="Rdoissue_CheckedChanged" Text="Issuing" />
                                          <asp:RadioButton ID="rdoReceive" runat="server" AutoPostBack="True" 
                                              CssClass="style1" oncheckedchanged="rdoReceive_CheckedChanged" 
                                              Text="Receiving" />
                                          <br />
                                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                          <asp:CheckBox ID="chk_can" runat="server" AutoPostBack="True" CssClass="style5" 
                                              oncheckedchanged="chk_can_CheckedChanged" Text="Can" />
                                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                          <asp:CheckBox ID="chk_dpu" runat="server" AutoPostBack="True" CssClass="style5" 
                                              oncheckedchanged="chk_dpu_CheckedChanged" Text="Dpu Sys" />
                                      </asp:Panel>
                                      <br />
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right" width="20%">
                                      <asp:Label ID="Label43" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Plant Name"></asp:Label>
                                  </td>
                                  <td align="left" width="30%">
                                      <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                                          class="ddl2" CssClass="ddl2" Height="30px" 
                                          onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="235px">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label21" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Agent Id/Can No"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:DropDownList ID="ddl_Agentid" runat="server" AutoPostBack="True" 
                                          class="ddl2" CssClass="ddl2" Height="30px" 
                                          onselectedindexchanged="ddl_Agentid_SelectedIndexChanged" Width="235px">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label50" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Issue/Receive Date"></asp:Label>
                                  </td>
                                  <td align="LEFT">
                                      <asp:TextBox ID="txt_date" runat="server" CssClass="ddl" Width="234px"></asp:TextBox>
                                      <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" 
                                          Format="dd/MM/yyyy" PopupButtonID="txt_joining" PopupPosition="TopRight" 
                                          TargetControlID="txt_date">
                                      </asp:CalendarExtender>
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label53" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="CanType"></asp:Label>
                                  </td>
                                  <td align="LEFT">
                                      <asp:DropDownList ID="ddl_cantype" runat="server" 
                                          class="ddl2" CssClass="ddl2" Height="30px" 
                                          onselectedindexchanged="ddl_Agentid_SelectedIndexChanged" Width="235px">
                                          <asp:ListItem>--------------Select--------------</asp:ListItem>
                                          <asp:ListItem>20KG</asp:ListItem>
                                          <asp:ListItem>40KG</asp:ListItem>
                                          <asp:ListItem>10KG</asp:ListItem>
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label55" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Dpu Type"></asp:Label>
                                  </td>
                                  <td align="LEFT">
                                      <asp:DropDownList ID="ddl_DpuSys" runat="server" 
                                          class="ddl2" CssClass="ddl2" Height="30px" Width="235px">
                                          <asp:ListItem>--------------Select--------------</asp:ListItem>
                                          <asp:ListItem>DPUSYS</asp:ListItem>
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label51" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Issuer/receiver Name"></asp:Label>
                                  </td>
                                  <td align="LEFT">
                                      <asp:TextBox ID="txt_Canissuorrecname" runat="server" CssClass="ddl2" 
                                          Width="230px"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                          ControlToValidate="txt_Canissuorrecname" ErrorMessage="Fill" ForeColor="Red"></asp:RequiredFieldValidator>
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label52" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Noofcans"></asp:Label>
                                  </td>
                                  <td align="LEFT">
                                      <asp:TextBox ID="txt_Noofcans" runat="server" CssClass="ddl2" 
                                          Width="230px"></asp:TextBox>
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label54" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="NoofDpuSys"></asp:Label>
                                  </td>
                                  <td align="LEFT">
                                      <asp:TextBox ID="txt_NoofDpu" runat="server" CssClass="ddl2" Width="230px"></asp:TextBox>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      &nbsp;</td>
                                  <td align="LEFT">
                                      <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                                          OnClientClick="return validate();" Text="Submit" />
                                      <asp:Button ID="Button2" runat="server" BorderColor="#663300" 
                                          BorderStyle="Inset" BorderWidth="1px" CssClass="buttonclass" Font-Bold="True" 
                                          Height="23px" OnClientClick="return PrintPanel();" Text="Print" />
                                      <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="True" 
                                          CssClass="tb10" Font-Size="X-Small" Height="20px" 
                                          onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Visible="False" 
                                          Width="70px">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      &nbsp;</td>
                                  <td align="LEFT">
                                      <asp:Label ID="lbl_msg" runat="server" 
                                          style="color: #009900; font-weight: 700;"></asp:Label>
                                  </td>
                              </tr>
                          </table>  </center> 
                      <br />
                      
                      
                      
                      </fieldset> </center>
                      </asp:Panel>
                  </td>
              </tr>
              </table>
      

      </fieldset>
      
          <br />
      <div>
      </panel>
         <asp:Panel id="pnlContents" runat = "server"> 
        
                   
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            CssClass="GridViewStyle1" HeaderStyle-BackColor="#61A6F8" ShowFooter="True"
                            PageSize="5" Font-Size="12px" 
              onpageindexchanging="GridView1_PageIndexChanging" 
                            onrowdatabound="GridView1_RowDataBound" ondatabound="GridView1_DataBound">
                            <Columns>
                     
                                <asp:BoundField DataField="Plant_Name" HeaderText="PlantName"  HeaderStyle-ForeColor="brown"
                                    SortExpression="Plant_Name" >
                                <HeaderStyle ForeColor="Brown" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Agent_Id" HeaderText="AgentId"   HeaderStyle-ForeColor="brown"
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
                           <asp:Label ID="rrr"  runat="server" Text="TotCans"  ForeColor="Magenta" Font-Bold="true"></asp:Label> 
                            <asp:Label ID="lblTotalcanissue" runat="server" Font-Bold="true"  ForeColor="Green"></asp:Label>

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
                                <asp:Label ID="rrr1"  runat="server" Text="TotCans"  ForeColor="Magenta" Font-Bold="true"></asp:Label> 
                            <asp:Label ID="lblTotalCanReceiving" runat="server" ForeColor="Green"></asp:Label>
                            <%-- <asp:Label ID="Label1" runat="server" Font-Bold="true" ForeColor="Green">Grand Total:</asp:Label><br />--%>
			              	</div>
			                    </FooterTemplate>
		                               <HeaderStyle     ForeColor="Brown" />
		                       </asp:TemplateField>



                                    
                                 




                                    
                                  <asp:BoundField DataField="CanType" HeaderText="CanType"  HeaderStyle-ForeColor="brown"
                                    SortExpression="CanType" />



                                    
                                 




                                    
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
                           <asp:Label ID="rrr21"  runat="server" Text="TotDpuSys"  ForeColor="Magenta" Font-Bold="true"></asp:Label> 
                            <asp:Label ID="lblTotalDpuissue" runat="server"  ForeColor="Green"></asp:Label>

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
                           <asp:Label ID="rrr11"  runat="server" Text="TotDpuSys"  ForeColor="Magenta" Font-Bold="true"></asp:Label> 
                            <asp:Label ID="lblTotalDpurec" runat="server"  ForeColor="Green"></asp:Label>

			              	</div>
			                    </FooterTemplate>
		                              <HeaderStyle ForeColor="Brown" />
		                       </asp:TemplateField>




                                    
                                <asp:BoundField DataField="DpuType" HeaderText="DpuType"  HeaderStyle-ForeColor="brown"
                                    SortExpression="DpuType" />




                                    
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

