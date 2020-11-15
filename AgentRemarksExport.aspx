<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgentRemarksExport.aspx.cs" Inherits="AgentRemarksExport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <style type="text/css">
     
     
     .style1
     {
         
         width:500px;
         text-align:center;
         
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

        
        
    </style>



    <style type="text/css">
body
{
    margin: 0;
    padding: 0;
    height: 100%;
}
.modal
{
    display: none;
    position: absolute;
    top: 0px;
    left: 0px;
    background-color: black;
    z-index: 100;
    opacity: 0.8;
    filter: alpha(opacity=60);
    -moz-opacity: 0.8;
    min-height: 100%;
}
#divImage
{
    display: none;
    z-index: 1000;
    position: fixed;
    top: 0;
    left: 0;
    background-color: White;
    height: 550px;
    width: 600px;
    padding: 3px;
    border: solid 1px black;
}


.DataWebControlStyle
{
    font-size: 90%;
}

.AlternatingRowStyle
{
    background-color: #fcc;
}

.RowStyle
{
}

.HeaderStyle
{
    background-color: #900;
    color: White;
    font-weight: bold;
}

.SelectedRowStyle
{
    background-color: Yellow;
}

.FooterStyle
{
    background-color: #a33;
    color: White;
    text-align: right;
}

.PagerRowStyle
{
    background-color: #ddd;
    text-align: right;
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
 <div style="position: fixed; text-align: center; height:100%; width: 1%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
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
                    
                      &nbsp;</td>
                  <td  width=20% style="width: 40%">
               <center>     
                   <asp:Label ID="Label18" runat="server" CssClass="style4" EnableTheming="False" 
                        Font-Size="Small" 
                        style="font-family: Andalus; font-size: medium; font-weight: 700;" 
                        Text="Agent Remark Update  Status "></asp:Label> 
                        </center>
                        
                        </td>
                  <td  width=20%>
                      &nbsp;</td>
              </tr>
              <tr>
                  <td colspan="3" class="style9">
                      <asp:Panel ID="Panel9" runat="server" BorderStyle="Inset">
                      <center>
                          <br />
                      <fieldset   class="style1"    style="background-color: #CCFFFF">
                          <table class="style2">
                              <tr>
                                  <td width=20% class="style3">
                                      </td>
                                  <td width=20% align="right" class="style3">
                                      <asp:Label ID="Label43" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Plant Name"></asp:Label>
                                  </td>
                                  <td width=30% align="left" class="style3">
                                      <asp:DropDownList ID="ddl_PlantName" runat="server" 
                                          class="ddl2" CssClass="ddlstyle" Height="30px" Width="230px">
                                      </asp:DropDownList>
                                  </td>
                                  <td width=15% class="style3">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td colspan="4">
                                      <asp:Label ID="From" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="From Date"></asp:Label>
                                      <asp:TextBox ID="txt_FromDate" runat="server" Font-Size="Small" Height="20px" 
                                          Width="125px"></asp:TextBox>
                                      <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                                          Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="BottomRight" 
                                          TargetControlID="txt_FromDate">
                                      </asp:CalendarExtender>
                                      <asp:Label ID="To" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="To Date"></asp:Label>
                                      <asp:TextBox ID="txt_ToDate" runat="server" Font-Size="Small" Height="20px" 
                                          Width="125px"></asp:TextBox>
                                      <asp:CalendarExtender ID="txt_ToDate_CalendarExtender2" runat="server" 
                                          Format="dd/MM/yyyy" PopupButtonID="txt_ToDate" PopupPosition="BottomRight" 
                                          TargetControlID="txt_ToDate">
                                      </asp:CalendarExtender>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      &nbsp;</td>
                                  <td align="LEFT">
                                      <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                                          OnClientClick="return validate();" Text="Submit" CssClass="buttonclass" />
                                          
                                    <asp:Button ID="Button2" runat="server" 
                        BorderStyle="Inset" Font-Bold="True" 
                        OnClientClick="return PrintPanel();" Text="Print" CssClass="buttonclass" 
                        BorderColor="#663300" BorderWidth="1px" Height="23px"  />
                                      <asp:Button ID="Button3" runat="server" CssClass="buttonclass" 
                                          onclick="Button3_Click" Text="Export" />
                                  </td>
                                  

                                  <td>


                                      &nbsp;</td>
                              </tr>
                          </table>
                      
                      
                      
                      </fieldset>
                          <br />
                          <asp:CheckBox ID="CheckBox1" runat="server" 
                              style="font-size: x-small; font-family: Arial; color: #990033; font-weight: 700" 
                              Text="Click To Increase" />
                      </center>
                      </asp:Panel>
                  </td>
              </tr>
              </table>
      

      </fieldset>
      
          <br />
      <div>
      </panel>
         <asp:Panel id="pnlContents" runat = "server"> 
        
                   
                
        
                   
                 <center>
                                                                     <table>
                                                                         <tr align="center">
                                                                             <td align="center">

                                                                                 <asp:Label ID="Label8" runat="server" Text="VYSHNAVI DAIRY SPECIALITIES" 
                                                                                     style="font-weight: 700; font-family: Andalus; color: #990033;"></asp:Label>


                                                                                 </td>
                                                                         </tr>
                                                                     </table


                                                                      <table>
                                                                         <tr align="left">
                                                                             <td align="left">

                                                                                 <asp:Label ID="Label14" runat="server" Text="Agent Remark Deatils" 
                                                                                     style="font-weight: 700; font-family: Andalus; color: #990033;"></asp:Label>


                                                                                 <br />
                                                                                 <asp:Label ID="Label44" runat="server" 
                                                                                     style="font-weight: 700; font-family: Andalus; color: #990033;"></asp:Label>
                                                                                 <asp:Label ID="Label45" runat="server" 
                                                                                     style="font-weight: 700; font-family: Andalus; color: #990033;"></asp:Label>


                                                                                 </td>
                                                                         </tr>
                                                                     </table>


                                                                     <br />
                     <asp:GridView ID="GridView1" runat="server" CssClass="gridview2" Font-Size="12px" onrowdatabound="GridView1_RowDataBound">
                     
                     </asp:GridView>


                                                                     <br />




                                                                   </center>

                               </asp:panel>
                  </div>
     </center>


     <div id="divBackground" class="modal">
</div>
<div id="divImage">
<table style="height: 75%; width: 75%">
    <tr>
        <td valign="middle" align="center">
            <img id="imgLoader" alt="" src="images/loader.gif" />
            <img id="imgFull" alt="" src="" style="display: none; height: 500px; width: 590px" />
        </td>
    </tr>
    <tr>
        <td align="center" valign="bottom">
            <input id="btnClose" type="button" value="close" onclick="HideDiv()" />
        </td>
    </tr>
</table>
</div>




     <br />

              </ContentTemplate>
           <Triggers>
<asp:PostBackTrigger ControlID="Button3" />
</Triggers>
        </asp:UpdatePanel>






 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

