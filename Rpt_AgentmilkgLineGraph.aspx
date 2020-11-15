<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Rpt_AgentmilkgLineGraph.aspx.cs" Inherits="Rpt_AgentmilkgLineGraph" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript" language="JavaScript" src="FusionCharts/FusionCharts.js"></script>
      <script type="text/javascript" language="JavaScript">
          function myJS(myVar) {
              window.alert(myVar);
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
            <legend style="color: #3399FF">AgentLine Graph</legend>
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
                  
                        <asp:DropDownList ID="ddl_PlantName" runat="server" Font-Bold="true"   
                            Font-Size="12px"  Width="150px" AutoPostBack="True" 
                            onselectedindexchanged="ddl_PlantName_SelectedIndexChanged">
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
                            <td align="left">
                            </td>
                            <td align="left">
                                <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_ToDate" runat="server" CssClass="tb10" Font-Size="X-Small" TabIndex="5"  Width="145px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                                    PopupButtonID="txt_ToDate" PopupPosition="BottomRight" 
                                    TargetControlID="txt_ToDate">
                                </asp:CalendarExtender>
                                <asp:Button ID="btn_LoadAgent" runat="server" BackColor="#006699" 
                                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px"   
                                    Text="LoadAgent" onclick="btn_LoadAgent_Click" />
                            </td>
                        </tr>
                         <tr>
                            <td align="left">
                            </td>
                            <td align="left">
                                <asp:Label ID="Lbl_AgentName" runat="server" CssClass="style7" Font-Size="Small" 
                                    style="font-family: Andalus; font-size: medium" Text="AgentName"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddl_AgentNAme" runat="server" Font-Bold="true" 
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
                                    onclick="btn_Generate_Click" Text="Graph" />
                                <asp:Button ID="btn_print" runat="server" BackColor="#006699"
                                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                                    OnClientClick="return PrintPanel();" Text="Print" Visible="False" />
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
                   <br />
                    </center> 
                   </span>
               </asp:Panel>
                    </center>
        </td>
        </tr>       
        </table>
        
<div>
       
    </div>  
      <div>
      <asp:Literal ID="FCLiteral1" runat="server"></asp:Literal>
    </div>
        
    </fieldset>
   
    </asp:Panel>
    </div>
         </ContentTemplate>
         <Triggers>
<%--<asp:PostBackTrigger ControlID="btn_Export" />
<asp:PostBackTrigger ControlID="btn_VmccExport" />--%>
</Triggers>

        </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

