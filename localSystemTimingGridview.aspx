<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="localSystemTimingGridview.aspx.cs" Inherits="localSystemTimingGridview"  EnableEventValidation = "false" %>
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

.blink 
{ 

text-decoration:blink;
            color: #FF9900;
        } 

.blinkytext {
     font-family: Arial, Helvetica, sans-serif;
     font-size: 1.2em;
     text-decoration: blink;
     font-style: normal;
     background-color:Red;
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
            text-align: right;
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
        .style4
        {
            font-family: Andalus;
            color: #000000;
            font-weight: 700;
        }
        .style8
        {
        }
        .style10
        {
            color: black;
            font-family: serif;
        }
        .style11
        {
            color: #663300;
        }
        .style12
        {
            width: 100%;
        }
        </style>
        <!--
         <script type="text/javascript">
             function openWindow(Plant_Code) {
                 window.open('page.aspx?Plant_Code=' + Plant_Code, 'open_window', ' width=640, height=480, left=0, top=0');
             }
   </script>
    <script type="text/javascript">
        function blink(id, delay) {
            var s = document.getElementById(id).style;
            s.visibility = (s.visibility == "" ? "hidden" : "");
            setTimeout(blink, delay);
        }
        attachEvent("onload", "blink('myLabel', 300)");
</script>

-->


<%--
        <asp:BoundField DataField="Plant_Code" HeaderText="PlantCode" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="Plant_Name" HeaderText="PlantName" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="Name" HeaderText="NAME" ItemStyle-Width="150" >
                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="USERID" HeaderText="USERID" ItemStyle-Width="150" >
                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>
         <asp:BoundField DataField="TIME" HeaderText="TIME" ItemStyle-Width="150" >
                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="IP" HeaderText="IP" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="WEINAME" HeaderText="WEINAME" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="WEITIME" HeaderText="WEITIME" ItemStyle-Width="150" >
                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="lABNAME" HeaderText="lABNAME" ItemStyle-Width="150" >

                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>

        <asp:BoundField DataField="LABTIME" HeaderText="LABTIME" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="NETWORKSYSTEM" HeaderText="NETWORKSYSTEM" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
                                    --%>


    <script type="text/javascript">
        function blink(id, delay) {
            var s = document.getElementById(id).style;
            s.visibility = (s.visibility == "" ? "hidden" : "");
            setTimeout(blink, delay);
        }
        attachEvent("onload", "blink('myLabel', 300)");
</script>




    





 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
          EnablePageMethods="true">
      </asp:ToolkitScriptManager>


  <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height:1%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>


<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>













      <center>
      <panel width=100%>
      
      <fieldset width=90% bgcolor="#CCFFFF">
      
      
          <table class="style12">
              <tr  align="right">
                  <td>
                      <asp:LinkButton ID="LinkButton1" runat="server" 
                          PostBackUrl="~/LocalSystemInformationReports.aspx">View Report</asp:LinkButton>
                  </td>
              </tr>
          </table>
      
      
          <br />
      
      
          <table style="width: 100%">
              <tr>
                  <td align="left" class="style8">
                    
                     
                      <center>
                          <asp:Label ID="Label18" runat="server" CssClass="style4" EnableTheming="False" 
                              Font-Size="Small" 
                              style="font-family: Andalus; font-size: medium; font-weight: 700;" 
                              Text="Plant Local System Timing"></asp:Label>
                      </center>
                    
                     
                  </td>
              </tr>
              <tr>
                  <td align="center" style8">
                      <span style="font-weight: bold;" class="style10">Date</span><span 
                          style="color: #FF0066; font-weight: bold;">:<asp:Literal ID="Literal17" 
                          runat="server"></asp:Literal>
                      </span><span class="style11" style="font-weight: bold;">
                      <asp:Label ID="Session" runat="server" Text="Session" 
                          style="color: #000000; font-family: Andalus"></asp:Label>
                      :</span><span style="color: #FF0066; font-weight: bold;"><asp:Literal 
                          ID="Literal19" runat="server"></asp:Literal>
                      </span></td>
              </tr>
              <tr>
                  <td  align="CENTER">
                      <asp:Panel ID="Panel9" runat="server" BorderStyle="Inset">
                          <div WIDTH=100%>
                              <asp:GridView ID="GridView1" runat="server" CssClass="gridcls" 
                                  Font-Size="Small" onrowcreated="GridView1_RowCreated" 
                                  onrowdatabound="GridView1_RowDataBound"  DataKeyNames="plantcode"
                                  onselectedindexchanged="GridView1_SelectedIndexChanged" 
                                  AutoGenerateColumns="False" onrowcommand="GridView1_RowCommand" 
                                  onselectedindexchanging="GridView1_SelectedIndexChanging" Width="100%">
                                    <EditRowStyle BackColor="#999999" />
                                                   <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                                   <HeaderStyle BackColor="#f4f4f4" Font-Bold="False" Font-Italic="False" 
                                                       Font-Names="Raavi" Font-Size="Small" ForeColor="Black" />
                                                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                   <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                                   <AlternatingRowStyle HorizontalAlign="Center" />
                                                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                   <Columns>

    <%--
        <asp:BoundField DataField="Plant_Code" HeaderText="PlantCode" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="Plant_Name" HeaderText="PlantName" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="Name" HeaderText="NAME" ItemStyle-Width="150" >
                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="USERID" HeaderText="USERID" ItemStyle-Width="150" >
                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>
         <asp:BoundField DataField="TIME" HeaderText="TIME" ItemStyle-Width="150" >
                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="IP" HeaderText="IP" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="WEINAME" HeaderText="WEINAME" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="WEITIME" HeaderText="WEITIME" ItemStyle-Width="150" >
                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="lABNAME" HeaderText="lABNAME" ItemStyle-Width="150" >

                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>

        <asp:BoundField DataField="LABTIME" HeaderText="LABTIME" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="NETWORKSYSTEM" HeaderText="NETWORKSYSTEM" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
                                    --%>

       <%-- <asp:BoundField DataField="OfficesystemIp" HeaderText="OfficesystemIp" ItemStyle-Width="150" >
                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>--%>
     <%--   <asp:BoundField DataField="WeighersystemIp" HeaderText="WeighersystemIp" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>--%>

                                       
                                      <asp:TemplateField HeaderText="SNO">
                <ItemTemplate>
                       <%#Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>           

                                       <asp:BoundField DataField="PlantCode" HeaderText="PlantCode" 
                                           SortExpression="PlantCode" />

                                            <asp:TemplateField HeaderText="PlantName">
        <ItemTemplate>
              <asp:LinkButton runat="server" ID="lnkView"  CssClass="linkNoUnderline" ForeColor="Brown" Text='<%# Eval("PlantName") %>'  OnClick="lnkView_Click"></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>
      <%--  <asp:BoundField DataField="PlantName" HeaderText="PlantName" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>--%>
        <asp:BoundField DataField="OfficeSys" HeaderText="Status" ItemStyle-Width="150" >
                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>
         <asp:BoundField DataField="OffTime" HeaderText="Officetime" ItemStyle-Width="150" >
                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="WeigherSys" HeaderText="Status" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="weiTime" HeaderText="Weighertime" ItemStyle-Width="150" >
                                       <ItemStyle Width="150px" />
                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>
        <asp:BoundField DataField="LabSys" HeaderText="Status" ItemStyle-Width="150" >

                                       <ItemStyle Width="150px" />
                                       </asp:BoundField>
                                            
      <asp:BoundField DataField="LabTime" HeaderText="Laptime" ItemStyle-Width="50" >
                                       <ItemStyle Width="50px" />
                                       </asp:BoundField>
                                     

   
     </Columns>

                              </asp:GridView>
                              <center>
                              <table width=75%>
                                  </center>
                                  <tr>
                                      <td valign=top>
                                          <table width="75%">
                                              <tr valign="top" width="25%">
                                                  <td align="center" width="25%">
                                                      <asp:GridView ID="GridView2" runat="server" CssClass="gridview2" 
                                                          ondatabound="GridView2_DataBound">
                                                      </asp:GridView>
                                                  </td>
                                                  <td align="center" width="75%">
                                                      <asp:GridView ID="GridView3" runat="server" CssClass="gridview2" 
                                                          ondatabound="GridView3_DataBound">
                                                      </asp:GridView>
                                                  </td>
                                                  <td align="center" width="75%">
                                                      <asp:GridView ID="GridView4" runat="server" CssClass="gridview2" 
                                                          ondatabound="GridView4_DataBound">
                                                      </asp:GridView>
                                                  </td>
                                              </tr>
                                          </table>
                                      </td>
                                  </tr>
                              </table>
                              </td>
                              </tr>
                              </table>
                              <asp:Button ID="BACK" runat="server" CssClass="button2222" onclick="BACK_Click" 
                                  Text="Back" />
                              <br />
                          </div>
                      </asp:Panel>

                  </td>
              </tr>
              </table>
      

      </fieldset>
      
          <br />
      <div>
          <asp:Timer ID="Timer2" runat="server" Interval="100000" ontick="Timer2_Tick">
          </asp:Timer>
          </panel>
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




        </ContentTemplate>
        </asp:UpdatePanel>
       
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

