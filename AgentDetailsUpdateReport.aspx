<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgentDetailsUpdateReport.aspx.cs" Inherits="AgentDetailsUpdateReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script runat="server">
   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
            height: 126px;
        }
         .style3
        {
        
      width:75%;
        
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
        .style4
        {
            height: 104px;
        }
        .style5
        {
            height: 115px;
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







  <%--  <asp:Image ID="Image" runat="server" Width="100px" Height="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Image") %>' />--%>


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
                  <td width=20% align="left">
                    
                      <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" 
                          PostBackUrl="~/AgentDetails.aspx">Back</asp:LinkButton>
                    
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
              <tr align=center>
                  <td colspan="3" class="style9">
                      <asp:Panel ID="Panel9" runat="server" BorderStyle="Inset">
                      <fieldset   class="style3" style="background-color: #CCFFFF">
                          <table class="style2" >
                              <tr align="right" valign="top">
                                  <td width="40%">
                                      <asp:Label ID="Label43" runat="server" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Plant Name"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                                          class="ddl2" CssClass="tb10" Font-Size="Small" Height="25px" 
                                          onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="230px">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr align="right" valign="top">
                                  <td width="40%">
                                      <asp:Button ID="Button1" runat="server" CssClass="buttonclass" 
                                          onclick="Button1_Click" OnClientClick="return validate();" Text="Submit" 
                                          Visible="False" />
                                  </td>
                                  <td align="left">
                                      <asp:RadioButton ID="rdosingle" runat="server" Checked="True" 
                                          oncheckedchanged="rdosingle_CheckedChanged" 
                                          style="font-size: medium; font-family: Andalus" Text="Single Agents" />
                                      <asp:RadioButton ID="Rtoall" runat="server" Enabled="False" 
                                          oncheckedchanged="Rtoall_CheckedChanged" 
                                          style="font-size: medium; font-family: Andalus; font-weight: 400" 
                                          Text="All Agents" />
                                  </td>
                              </tr>
                              <tr align="right" valign="top">
                                  <td width="40%">
                                      <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="True" 
                                          CssClass="tb10" Font-Size="X-Small" Height="20px" 
                                          onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Visible="False" 
                                          Width="70px">
                                      </asp:DropDownList>
                                      <asp:Label ID="Label21" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Agent Id/Can No"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:DropDownList ID="ddl_Agentid" runat="server" AutoPostBack="True" 
                                          class="ddl2" CssClass="tb10" Font-Size="Small" Height="25px" 
                                          onselectedindexchanged="ddl_Agentid_SelectedIndexChanged" Width="230px">
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr valign="top" align=right>
                                  <td width="40%">
                                      <asp:Label ID="Label44" runat="server" style="font-family: Andalus" 
                                          Text="Update Type"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:DropDownList ID="ddlupdatetype" runat="server" AutoPostBack="True" 
                                          class="ddl2" CssClass="tb10" Height="30px" 
                                          onselectedindexchanged="ddlupdatetype_SelectedIndexChanged" Width="230px" 
                                          Font-Size="Small">
                                          <asp:ListItem Value="0">----------Select---------</asp:ListItem>
                                          <asp:ListItem Value="1">AgentImage</asp:ListItem>
                                          <asp:ListItem Value="2">Aadhar Image</asp:ListItem>
                                          <asp:ListItem Value="3">RattionCard Image</asp:ListItem>
                                          <asp:ListItem Value="4">VoterId Image</asp:ListItem>
                                          <asp:ListItem Value="5">PanCardImage</asp:ListItem>
                                          <asp:ListItem Value="6">BankAccountImage</asp:ListItem>
                                          <asp:ListItem Value="7">Address</asp:ListItem>
                                          <asp:ListItem Value="8">Mobile No</asp:ListItem>
                                          <asp:ListItem Value="9">Aadhaar No</asp:ListItem>
                                          <asp:ListItem Value="10">Rationcard No</asp:ListItem>
                                          <asp:ListItem Value="11">VoterId No</asp:ListItem>
                                          <asp:ListItem Value="12">PanCard No</asp:ListItem>
                                          <asp:ListItem Value="13">Nominees Name</asp:ListItem>
                                      </asp:DropDownList>
                                  </td>
                              </tr>
                              <tr valign=top align="right">
                                  <td width="40%" align="left">
                                      <asp:FileUpload ID="FileUpload1" runat="server" Width="220px" />
                                      <br />
                                      <asp:Button ID="uploadthar" runat="server" CausesValidation="False" 
                                          onclick="uploadthar_Click" Text="Upload" />
                                      <asp:Label ID="lblmessage" runat="server" Text="Label"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:Image ID="updateimage" runat="server" Height="50px" Width="50px" 
                                          BorderStyle="Inset" BorderWidth="1px" ImageAlign="Top" />
                                  </td>
                              </tr>
                              <tr valign="top">
                                  <td valign="top" align=right>
                                      <asp:Label ID="Label47" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Mobile"></asp:Label>
                                  </td>
                                  <td valign="top" align="left">
                                      <asp:TextBox ID="txtmobile" runat="server" CssClass="tb10" Enabled="False" 
                                          Height="25px" Width="250px"></asp:TextBox>
                                  </td>
                              </tr>
                              <tr valign="top">
                                  <td valign="top" align=right>
                                      <br />
                                      <asp:Label ID="Label48" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Address"></asp:Label>
                                  </td>
                                  <td valign="top" align="left">
                                      <asp:TextBox ID="Address" runat="server" Height="59px" TextMode="MultiLine" 
                                          Width="154px"></asp:TextBox>
                                  </td>
                              </tr>
                              <tr valign="top">
                                  <td align="center" colspan="2" valign="top">
                                 <asp:Button ID="Button3" runat="server" onclick="Button3_Click"   
                                          Text="Update" CssClass="buttonclass" />  

                                           


                                      <asp:Button ID="Button2" runat="server" BorderColor="#663300" 
                                          BorderStyle="Inset" BorderWidth="1px" CssClass="buttonclass" Font-Bold="True" 
                                          Height="23px" OnClientClick="return PrintPanel();" Text="Print" />
                                      <br />
                                      <asp:Label ID="lblmsg" runat="server" 
                                          style="font-size: small; font-family: Andalus" Text="Label"></asp:Label>
                                  </td>
                              </tr>
                          </table>
                      
                      
                      
                      </fieldset>
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
                            PageSize="20" Font-Size="10px" 
              onpageindexchanging="GridView1_PageIndexChanging" onrowcreated="GridView1_RowCreated">
                            <Columns>
                              <%--  <asp:Image ID="Image" runat="server" Width="100px" Height="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Image") %>' />--%>
                     
                              <%--  <asp:BoundField DataField="Plant_Name" HeaderText="PName"  HeaderStyle-ForeColor="brown"
                                    SortExpression="Plant_Name" />
                                <asp:BoundField DataField="Agent_Id" HeaderText="CanNO"   HeaderStyle-ForeColor="brown"
                                    SortExpression="Agent_Id" />
                                <asp:BoundField DataField="Agent_Name" HeaderText="AName"   HeaderStyle-ForeColor="brown"
                                    SortExpression="Agent_Name" />
                                <asp:BoundField DataField="Route_Name" HeaderText="RName"   HeaderStyle-ForeColor="brown"
                                    SortExpression="Route_Name" />
                                <asp:BoundField DataField="JoiningDate" HeaderText="DOJ"   HeaderStyle-ForeColor="brown"
                                    SortExpression="JoiningDate" />
                                <asp:BoundField DataField="Address" HeaderText="Address"  HeaderStyle-ForeColor="brown"
                                    SortExpression="Address" />
                                <asp:BoundField DataField="AadharNo" HeaderText="AadharNo"  HeaderStyle-ForeColor="brown"
                                    SortExpression="AadharNo" />
                                <asp:BoundField DataField="BankName" HeaderText="BankName"  HeaderStyle-ForeColor="brown"
                                    SortExpression="BankName" />
                                <asp:BoundField DataField="BankAccNo" HeaderText="AccNo"  HeaderStyle-ForeColor="brown"
                                    SortExpression="BankAccNo" />
                                <asp:BoundField DataField="IfscNo" HeaderText="IfscNo"  HeaderStyle-ForeColor="brown"
                                    SortExpression="IfscNo" />
                                <asp:BoundField DataField="BranchName" HeaderText="BranName"   HeaderStyle-ForeColor="brown"
                                    SortExpression="BranchName" />
                                <asp:BoundField DataField="Mobile" HeaderText="Mobile"  HeaderStyle-ForeColor="brown"
                                    SortExpression="Mobile" />
                     
                           <asp:ImageField DataImageUrlField="Image" ControlStyle-Height="75px" ControlStyle-Width="75px" HeaderText="Image" HeaderStyle-ForeColor="brown" >
<ControlStyle Height="75px" Width="75px"></ControlStyle>
                                </asp:ImageField>               --%>
                            




                               
                                <asp:BoundField DataField="Agent_Id" HeaderText="CanNO"   HeaderStyle-ForeColor="brown"
                                    SortExpression="Agent_Id" />
                                <asp:BoundField DataField="Agent_Name" HeaderText="AName"   HeaderStyle-ForeColor="brown"
                                    SortExpression="Agent_Name" />
                         <%--        <asp:BoundField DataField="Route_Name" HeaderText="RName"   HeaderStyle-ForeColor="brown"
                                    SortExpression="Route_Name" />
                                <asp:BoundField DataField="JoiningDate" HeaderText="DOJ"   HeaderStyle-ForeColor="brown"
                                    SortExpression="JoiningDate" />   --%>
                                <asp:BoundField DataField="Address" HeaderText="Address"  HeaderStyle-ForeColor="brown"
                                    SortExpression="Address" />
                               <%--     <asp:BoundField DataField="AadharNo" HeaderText="AadharNo"  HeaderStyle-ForeColor="brown"
                                    SortExpression="AadharNo" /> --%>
                                <asp:BoundField DataField="BankName" HeaderText="BankName"  HeaderStyle-ForeColor="brown"
                                    SortExpression="BankName" />
                                <asp:BoundField DataField="BankAccNo" HeaderText="AccNo"  HeaderStyle-ForeColor="brown"
                                    SortExpression="BankAccNo" />
                                <asp:BoundField DataField="IfscNo" HeaderText="IfscNo"  HeaderStyle-ForeColor="brown"
                                    SortExpression="IfscNo" />


                                <%--        <asp:ImageField DataImageUrlField="Image" ControlStyle-Height="75px" ControlStyle-Width="75px" HeaderText="Image" HeaderStyle-ForeColor="brown" >
<ControlStyle Height="75px" Width="75px"></ControlStyle>
                                </asp:ImageField>  



                                      <asp:ImageField DataImageUrlField="Aadharimage" ControlStyle-Height="75px" ControlStyle-Width="75px" HeaderText="Aadharimage" HeaderStyle-ForeColor="brown" >
<ControlStyle Height="75px" Width="75px"></ControlStyle>
                                </asp:ImageField>  



                                      <asp:ImageField DataImageUrlField="Rationimage" ControlStyle-Height="75px" ControlStyle-Width="75px" HeaderText="Rationimage" HeaderStyle-ForeColor="brown" >
<ControlStyle Height="75px" Width="75px"></ControlStyle>
                                </asp:ImageField>  




                                      <asp:ImageField DataImageUrlField="voterimage" ControlStyle-Height="75px" ControlStyle-Width="75px" HeaderText="voterimage" HeaderStyle-ForeColor="brown" >
<ControlStyle Height="75px" Width="75px"></ControlStyle>
                                </asp:ImageField>  




                                      <asp:ImageField DataImageUrlField="pancardimage" ControlStyle-Height="75px" ControlStyle-Width="75px" HeaderText="pancardimage" HeaderStyle-ForeColor="brown" >
<ControlStyle Height="75px" Width="75px"></ControlStyle>
                                </asp:ImageField>  



                                      <asp:ImageField DataImageUrlField="Accountimage" ControlStyle-Height="75px" ControlStyle-Width="75px" HeaderText="Accountimage" HeaderStyle-ForeColor="brown" >
<ControlStyle Height="75px" Width="75px"></ControlStyle>
  </asp:ImageField>  



  --%>



       <asp:TemplateField HeaderText="Agentimage" HeaderStyle-ForeColor="Brown" >
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Image")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
    </asp:TemplateField>


                                
               


                                 <asp:TemplateField HeaderText="Accountimage" HeaderStyle-ForeColor="Brown">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl='<%# Eval("Aadharimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
    </asp:TemplateField>

                                
                 

                                  <asp:TemplateField HeaderText="Rationimage" HeaderStyle-ForeColor="Brown">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl='<%# Eval("Rationimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
    </asp:TemplateField>

                                
                
                                    <asp:TemplateField HeaderText="voterimage" HeaderStyle-ForeColor="Brown">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl='<%# Eval("voterimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
    </asp:TemplateField>

                     

                                <asp:TemplateField HeaderText="pancardimage" HeaderStyle-ForeColor="Brown">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl='<%# Eval("pancardimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
    </asp:TemplateField>




                                 
    <asp:TemplateField HeaderText="BankA/c Image" HeaderStyle-ForeColor="Brown">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl='<%# Eval("Accountimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
    </asp:TemplateField>




                               <%--     <asp:Image ID="Image" runat="server" Width="100px" Height="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Image") %>' />

                                  



                                      <asp:Image ID="Image1" runat="server" Width="100px" Height="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Aadharimage") %>' />


                                       <asp:Image ID="Image2" runat="server" Width="100px" Height="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Rationimage") %>' />



                                        <asp:Image ID="Image3" runat="server" Width="100px" Height="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "voterimage") %>' />



                                         <asp:Image ID="Image4" runat="server" Width="100px" Height="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "pancardimage") %>' />



                                          <asp:Image ID="Image5" runat="server" Width="100px" Height="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Accountimage") %>' />



                                         --  %> 






                               <%--     <asp:BoundField DataField="BranchName" HeaderText="BranName"   HeaderStyle-ForeColor="brown"
                                    SortExpression="BranchName" />
                              <asp:BoundField DataField="Mobile" HeaderText="Mobile"  HeaderStyle-ForeColor="brown"
                                    SortExpression="Mobile" /> --%>


           <%--                       <asp:TemplateField HeaderText="AgentImage">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Image")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
    </asp:TemplateField>



    <asp:TemplateField HeaderText="Preview Image">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Aadharimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
    </asp:TemplateField>


    <asp:TemplateField HeaderText="Preview Image">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Rationimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Preview Image">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("voterimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
        </ItemTemplate>
    </asp:TemplateField>    --%>  








     <%--   <asp:TemplateField HeaderText="AgentImage">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Image")%>'
                Width="75px" Height="75px"  />
        </ItemTemplate>
    </asp:TemplateField>



    <asp:TemplateField HeaderText="Aadharimage">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl='<%# Eval("Aadharimage")%>'
                Width="75px" Height="75px"  />
        </ItemTemplate>
    </asp:TemplateField>


    <asp:TemplateField HeaderText="Rationimage">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl='<%# Eval("Rationimage")%>'
                Width="75px" Height="75px"  />
        </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="voterimage">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl='<%# Eval("voterimage")%>'     Width="75px" Height="75px"  />
        </ItemTemplate>
    </asp:TemplateField> 





    <asp:TemplateField HeaderText="pancardimage">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl='<%# Eval("pancardimage")%>'
                Width="75px" Height="75px"  />
        </ItemTemplate>
    </asp:TemplateField>




     <asp:TemplateField HeaderText="Accountimage">
        <ItemTemplate>
            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl='<%# Eval("Accountimage")%>'
                Width="75px" Height="75px"  />
        </ItemTemplate>
    </asp:TemplateField>
    --%>









                            </Columns>

<HeaderStyle BackColor="#61A6F8"></HeaderStyle>
                            </asp:GridView>

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
 
<asp:PostBackTrigger ControlID="uploadthar"></asp:PostBackTrigger>

</Triggers>



        
        </asp:UpdatePanel>
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

