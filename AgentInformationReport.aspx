<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgentInformationReport.aspx.cs" Inherits="AgentInformationReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
        
        
        .submitinput
         { 
    -webkit-border-radius: 5px; 
    -moz-border-radius: 5px; 
    }
     
        .buttonclass
{
padding-left: 10px;
font-weight: bold;
            height: 26px;
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
        .style3
        {
            font-family: Andalus;
        }
        .style4
        {
            font-family: Andalus;
            color: #000000;
            font-weight: 700;
        }
        .style6
        {
            font-family: Andalus;
            color: #FF3300;
            font-weight: bold;
        }
        .style7
        {
            width: 135px;
        }
        .style8
        {
            width: 20%;
        }
        .style9
        {
            font-family: Andalus;
            color: #99FF33;
            font-weight: 700;
        }
        .style10
        {
            font-family: Andalus;
            color: #009900;
            font-weight: 700;
        }
        .style11
        {
            color: #000000;
        }
        .style12
        {
            font-family: Andalus;
            color: #000000;
        }
    </style>

    <script type="text/javascript">
        function blink(id, delay) {
            var s = document.getElementById(id).style;
            s.visibility = (s.visibility == "" ? "hidden" : "");
            setTimeout(blink, delay);
        }
        attachEvent("onload", "blink('myLabel', 300)");
</script>




<%--  <asp:Image ID="Image" runat="server" Width="100px" Height="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Image") %>' />--%>


    <script type="text/javascript">
        function blink(id, delay) {
            var s = document.getElementById(id).style;
            s.visibility = (s.visibility == "" ? "hidden" : "");
            setTimeout(blink, delay);
        }
        attachEvent("onload", "blink('myLabel', 300)");
</script>




    





    
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
        function LoadDiv(url) {
            var img = new Image();
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            var imgLoader = document.getElementById("imgLoader");
            imgLoader.style.display = "block";
            img.onload = function () {
                imgFull.src = img.src;
                imgFull.style.display = "block";
                imgLoader.style.display = "none";
            };
            img.src = url;
            var width = document.body.clientWidth;
            if (document.body.clientHeight > document.body.scrollHeight) {
                bcgDiv.style.height = document.body.clientHeight + "px";
            }
            else {
                bcgDiv.style.height = document.body.scrollHeight + "px";
            }
            imgDiv.style.left = (width - 650) / 2 + "px";
            imgDiv.style.top = "20px";
            bcgDiv.style.width = "100%";

            bcgDiv.style.display = "block";
            imgDiv.style.display = "block";
            return false;
        }
        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            if (bcgDiv != null) {
                bcgDiv.style.display = "none";
                imgDiv.style.display = "none";
                imgFull.style.display = "none";
            }
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
                  <td align="left" class="style8">
                    
                      <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" 
                          PostBackUrl="~/AgentDetails.aspx">Back</asp:LinkButton>
                    
                  </td>
                  <td  width=20% style="width: 30%">
               <center>     <asp:Label ID="Label18" runat="server" CssClass="style4" EnableTheming="False" 
                        Font-Size="Small" 
                        style="font-family: Andalus; font-size: medium; font-weight: 700;" 
                        Text="Agent Information "></asp:Label> 
                        </center>
                        
                        </td>
                  <td align="left" class="style8">
                      &nbsp;</td>
              </tr>
              <tr>
                  <td colspan="3" class="style9">
                      <asp:Panel ID="Panel9" runat="server" BorderStyle="Inset">
                      <fieldset   WIDTH="500PX" class="style3">
                      <center>
                          <table >
                              <tr align="center">
                                  <td width=20% colspan="2" style="width: 40%">
                                      <asp:Label ID="Label43" runat="server" CssClass="style12" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Plant Name"></asp:Label>
                                  </td>
                                  <td width=30% align="left">
                                      <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                                          class="ddl2" CssClass="ddl2" Height="30px" 
                                          onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="230px">
                                      </asp:DropDownList>
                                  </td>
                                  <td width=15%>
                                      <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="True" 
                                          CssClass="tb10" Font-Size="X-Small" Height="20px" 
                                          onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Visible="False" 
                                          Width="70px">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr align="center">
                                  <td colspan="4">
                                      <asp:RadioButton ID="Rtoall" runat="server" AutoPostBack="True" Checked="True" 
                                          CssClass="style11" oncheckedchanged="Rtoall_CheckedChanged" 
                                          Text="Running Agents" />
                                      <asp:RadioButton ID="totagents" runat="server" AutoPostBack="True" 
                                          CssClass="style11" oncheckedchanged="totagents_CheckedChanged" 
                                          Text="All Agents" />
                                      <asp:RadioButton ID="rdosingle" runat="server" AutoPostBack="True" 
                                          CssClass="style11" oncheckedchanged="rdosingle_CheckedChanged" 
                                          Text="Single Agents" />
                                  </td>
                              </tr>
                              <tr align="right">
                                  <td colspan="2">
                                      <asp:Label ID="Label21" runat="server" CssClass="style12" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Agent Id/Can No"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:DropDownList ID="ddl_Agentid" runat="server" AutoPostBack="True" 
                                          class="ddl2" CssClass="ddl2" Height="30px" 
                                          onselectedindexchanged="ddl_Agentid_SelectedIndexChanged" Width="230px">
                                      </asp:DropDownList>
                                  </td>
                                  <td>
                                      &nbsp;</td>
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
                          </center>
                      <br />
                      
                      
                      
                      </fieldset>
                          <asp:Label ID="Label54" runat="server" CssClass="style11" Font-Bold="False" 
                              ForeColor="#CC0099" style="font-weight: 400; font-family: serif;" 
                              Visible="False"></asp:Label>
                          <br />
                           
                          <table class="style2">
                              <tr>
                                  <td align="left">
                                      <asp:Label ID="Label44" runat="server" CssClass="style10" Text="Tot Agents"></asp:Label>
                                      :<asp:Label ID="Label45" runat="server" CssClass="style4"></asp:Label>
                                  </td>
                                  <td  align="left">
                                      <asp:Label ID="Label46" runat="server" CssClass="style10" 
                                          Text="Complete Agents"></asp:Label>
                                      :<asp:Label ID="Label47" runat="server" Text="Label" CssClass="style11" 
                                          style="font-weight: 700"></asp:Label>
                                  </td>
                                  <td  align="left" width=30%>
                                      <asp:LinkButton ID="Label48" runat="server" onclick="Label48_Click">Incomplete Agents</asp:LinkButton>
                                      <asp:Label ID="Label49" runat="server" 
                                          style="text-decoration:blink; color: #FF9900; font-weight: 700;" ></asp:Label>
                                  </td>
                                  <td  align="left" class="style7">
                                      <asp:LinkButton ID="Label50" runat="server" onclick="Label50_Click">Pending List</asp:LinkButton>
                                      <asp:Label ID="Label51" runat="server" CssClass="style6" Text="Label"></asp:Label>
                                  </td>
                              </tr>
                          </table>
                        
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
                            CssClass="GridViewStyle1" HeaderStyle-BackColor="#61A6F8" 
                            PageSize="5" Font-Size="10px" 
              onpageindexchanging="GridView1_PageIndexChanging" onrowcreated="GridView1_RowCreated">
                            <Columns>
                              <%--  <asp:Image ID="Image" runat="server" Width="100px" Height="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Image") %>' />--%>
                     
                           
                              <asp:TemplateField HeaderText="SNo" HeaderStyle-ForeColor="brown">
     <ItemTemplate>
       <%#Container.DataItemIndex + 1 %>
     </ItemTemplate>
    </asp:TemplateField>


                              <%-- <asp:BoundField DataField="Plant_Name" HeaderText="PName"  HeaderStyle-ForeColor="brown"
                                    SortExpression="Plant_Name" />--%>
                                <asp:BoundField DataField="Agent_Id" HeaderText="CanNO"   HeaderStyle-ForeColor="brown"
                                    SortExpression="Agent_Id" >
                                <HeaderStyle ForeColor="Brown" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Agent_Name" HeaderText="AName"   HeaderStyle-ForeColor="brown"
                                    SortExpression="Agent_Name" >
                                <HeaderStyle ForeColor="Brown" />
                                </asp:BoundField>
                         <%--        <asp:BoundField DataField="Route_Name" HeaderText="RName"   HeaderStyle-ForeColor="brown"
                                    SortExpression="Route_Name" />
                                <asp:BoundField DataField="JoiningDate" HeaderText="DOJ"   HeaderStyle-ForeColor="brown"
                                    SortExpression="JoiningDate" />   --%>
                                <asp:BoundField DataField="Address" HeaderText="Address"  HeaderStyle-ForeColor="brown" 
                                    SortExpression="Address" >
                               
                                <HeaderStyle ForeColor="Brown" Width="150px" />
                                <ItemStyle Width="150px" />
                                </asp:BoundField>
                               <%--     <asp:BoundField DataField="AadharNo" HeaderText="AadharNo"  HeaderStyle-ForeColor="brown"
                                    SortExpression="AadharNo" /> --%>
                                <asp:BoundField DataField="BankName" HeaderText="BankName"  HeaderStyle-ForeColor="brown"
                                    SortExpression="BankName" >
                                <HeaderStyle ForeColor="Brown" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BankAccNo" HeaderText="AccNo"  HeaderStyle-ForeColor="brown"
                                    SortExpression="BankAccNo" >
                                <HeaderStyle ForeColor="Brown" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IfscNo" HeaderText="IfscNo"  HeaderStyle-ForeColor="brown"
                                    SortExpression="IfscNo" >
                            



                    

                                   <HeaderStyle ForeColor="Brown" />
                                </asp:BoundField>
                            



                    

                                   <asp:TemplateField HeaderText="Agentimage" HeaderStyle-ForeColor="Brown" >
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Image")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
                                       <HeaderStyle ForeColor="Brown" />
    </asp:TemplateField>


                                
               


                                 <asp:TemplateField HeaderText="Aadharimage" HeaderStyle-ForeColor="Brown">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl='<%# Eval("Aadharimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
                                     <HeaderStyle ForeColor="Brown" />
    </asp:TemplateField>

                                
                 

                                  <asp:TemplateField HeaderText="Rationimage" HeaderStyle-ForeColor="Brown">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl='<%# Eval("Rationimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
                                      <HeaderStyle ForeColor="Brown" />
    </asp:TemplateField>

                                
                
                                    <asp:TemplateField HeaderText="voterimage" HeaderStyle-ForeColor="Brown">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl='<%# Eval("voterimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
                                        <HeaderStyle ForeColor="Brown" />
    </asp:TemplateField>

                     

                                <asp:TemplateField HeaderText="pancardimage" HeaderStyle-ForeColor="Brown">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl='<%# Eval("pancardimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
                                    <HeaderStyle ForeColor="Brown" />
    </asp:TemplateField>




                                 
    <asp:TemplateField HeaderText="BankA/c Image" HeaderStyle-ForeColor="Brown">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl='<%# Eval("Accountimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
        <HeaderStyle ForeColor="Brown" />
    </asp:TemplateField>









                            </Columns>

<HeaderStyle BackColor="#61A6F8"></HeaderStyle>
                            </asp:GridView>

                                  <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>

                <center>
                     <asp:GridView ID="GridView2" runat="server" CssClass="gridview1"  
                         HeaderStyle-ForeColor="Brown" Font-Size="10px" 
                         onrowcreated="GridView2_RowCreated">
                        <Columns>
                              <%--  <asp:Image ID="Image" runat="server" Width="100px" Height="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Image") %>' />--%>
                     
                           
                              <asp:TemplateField HeaderText="SNo">
     <ItemTemplate>
       <%#Container.DataItemIndex + 1 %>
     </ItemTemplate>
                                  <HeaderStyle />
    </asp:TemplateField>
    </Columns>

                         <HeaderStyle ForeColor="#003300" />

                        </asp:GridView>
                     <asp:Label ID="Label53" runat="server" CssClass="style11" Font-Bold="False" 
                         style="font-weight: 400; font-family: serif;" ForeColor="#CC0099" 
                         Visible="False"></asp:Label>
                        </center>

                  <div>
        <asp:Panel ID="pnModelPopup" runat="server" CssClass="popup">
            <table>
            </table>
        </asp:Panel>
        <div style="margin-left: 300px">
       <%--     <asp:LinkButton ID="lnkbtnSignin" runat="server">Sign in</asp:LinkButton>--%>
        </div>
    </div>

                     
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




        </ContentTemplate>
                        <Triggers>
<asp:PostBackTrigger ControlID="Button3" />
</Triggers>
        </asp:UpdatePanel>
       
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

