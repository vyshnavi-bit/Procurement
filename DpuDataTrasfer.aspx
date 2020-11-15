<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DpuDataTrasfer.aspx.cs" Inherits="DpuDataTrasfer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">




    <style type="text/css">
        .style1
        {
            font-family: Andalus;
        }
        .style2
        {
            font-size: small;
        }
        .buttonclass
        {
            font-weight: 700;
            height: 26px;
        }
        .style15
        {
            
           width:500px; 
        }
    </style>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
          EnablePageMethods="true">
      </asp:ToolkitScriptManager>

      <div>


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
                  <td width=20% align="left">
                    
                     
                    
                  </td>
                  <td  width=20% style="width: 40%">
               <center>     
                   <asp:Label ID="Label18" runat="server" CssClass="style4" EnableTheming="False" 
                        Font-Size="Small" 
                        style="font-family: Andalus; font-size: medium; font-weight: 700;" 
                        Text="Dpu Centres"></asp:Label> 
                        </center>
                        
                        </td>
                  <td  width=20%>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td colspan="3" class="style9">
                      <asp:Panel ID="Panel9" runat="server" BorderStyle="Inset">
                      <center>
                      <fieldset   class="style15"   style="background-color: #CCFFFF">
                          <table class="style2">
                              <tr align="center">
                                  <td width=20% align="right"  >
                                      &nbsp;</td>
                                  <td width=30% align="left"  >
                                      &nbsp;</td>
                                  <td width=15% class="style3">
                                    
                                      &nbsp;</td>
                              </tr>
                              <tr align="center">
                                  <td align="right" width="20%">
                                      <asp:Label ID="Label43" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Plant Name"></asp:Label>
                                  </td>
                                  <td align="left" width="30%">
                                      <asp:DropDownList ID="ddl_PlantName" runat="server" class="ddl2" 
                                          CssClass="ddl2" Height="30px" Width="200px">
                                      </asp:DropDownList>
                                  </td>
                                  <td class="style3" width="15%">
                                  </td>
                              </tr>
                              <tr class="style1">
                                  <td  align="right" >
                                      <asp:Label ID="Label44" runat="server" EnableTheming="False" Font-Size="Small" 
                                          style="font-family: Andalus; font-size: medium;" Text="Date"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:TextBox ID="txt_date" runat="server" CssClass="tb10" Font-Size="Small" 
                                          TabIndex="4"></asp:TextBox>
                                      <asp:DropDownList ID="ddl_shift" runat="server" class="ddl2" CssClass="ddl2" 
                                          Height="30px" Width="75px">
                                          <asp:ListItem>AM</asp:ListItem>
                                          <asp:ListItem>PM</asp:ListItem>
                                      </asp:DropDownList>
                                      <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" 
                                          Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                          TargetControlID="txt_date">
                                      </asp:CalendarExtender>
                                  </td>
                                  <td class="style2">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      &nbsp;</td>
                                  <td align="left">
                                      <asp:Button ID="Button1" runat="server" CssClass="buttonclass" 
                                          onclick="Button1_Click" OnClientClick="return validate();" Text="Submit" 
                                          Visible="False" />
                                      <asp:Button ID="Button2" runat="server" BorderColor="#663300" 
                                          BorderStyle="Inset" BorderWidth="1px" CssClass="buttonclass" Font-Bold="True" 
                                          Height="23px" OnClientClick="return PrintPanel();" Text="Print" 
                                          Visible="False" />
                                      <asp:Button ID="btn_ImportVmcc" runat="server" BackColor="#6F696F" ForeColor="White" 
                                          Height="26px" Text="Import" Width="50px" onclick="btn_ImportVmcc_Click" />
                                  </td>
                                  <td>
                                      <asp:CheckBox ID="Single" runat="server" AutoPostBack="True" CssClass="style2" 
                                          oncheckedchanged="Single_CheckedChanged" Text="Single" Visible="False" />
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label21" runat="server" EnableTheming="False" Font-Size="Small" 
                                          style="font-family: Andalus; font-size: medium;" Text="Dpu Agent" 
                                          Visible="False"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:DropDownList ID="DdlAgentlist" runat="server" AppendDataBoundItems="True" 
                                          AutoPostBack="True" class="ddl2" CssClass="ddl2" Height="30px" Visible="False" 
                                          Width="200px">
                                      </asp:DropDownList>
                                  </td>
                                  <td>
                                      <asp:CheckBox ID="All" runat="server" Checked="True" CssClass="style2" 
                                          Enabled="False" Text="All" Visible="False" />
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      &nbsp;</td>
                                  <td align="left">
                                      <asp:Label ID="lblmsg" runat="server" style="font-family: Andalus" Text="Label"></asp:Label>
                                  </td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                          </table>
                      <br />
                      
                      
                      
                      </fieldset>
                      </center>
                      </asp:Panel>
                  </td>
              </tr>
              </table>
      

      </fieldset>
      
          <br />
      <div>
      </panel>
                  </div>
     </center>






     <br />

        </ContentTemplate>
        
        </asp:UpdatePanel>
        

        </div>
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

