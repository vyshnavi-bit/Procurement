<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgentDetailsReports.aspx.cs" Inherits="AgentDetailsReports" %>
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
        .style2
        {
            width: 100%;
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



.Grid { border: solid 1px #FFFFFF; }.Grid td{border: solid 1px #FFFFFF;margin: 1px 1px 1px 1px;padding: 1px 1px 1px 1px;text-align: center;}.GridHeader{font-weight: bold;background-color: #8b8dbb;}.GridItem{background-color: #e6e6e6;} .GridAltItem{background-color: white;}




</style>



<%--  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script type = "text/javascript">
        $(".pic").live("mouseover, mousemove", function (e) {
            var dv = $("#popup");
            if (dv.length == 0) {
                var src = $(this)[0].src;
                var dv = $("<div />").css({ height: 100, width: 100, position: "absolute" });
                var img = $("<img />").css({ height: 100, width: 100 }).attr("src", src);
                dv.append(img);
                dv.attr("id", "popup");
                $("body").append(dv);
            }
            dv.css({ left: e.pageX, top: e.pageY });
        });
        $(".pic").live("mouseout", function () {
            $("#popup").remove();
        });
        
 
    </script>
    --%>






    <style type="text/css">        .rounded_corners    
            {            border: 1px solid #A1DCF2;      
                               -webkit-border-radius: 8px;     
                                      -moz-border-radius: 8px;    
                                              border-radius: 8px;       
                                                   overflow: hidden;    
                                                       }     
                                                          .rounded_corners td, .rounded_corners th    
                                                              {            border: 1px solid #A1DCF2;   
                                                                                    font-family: Arial;  
                                                                                              font-size: 10pt; 
                                                                                                         text-align: center;   
                                                                                                              }    
                                                                                                                  .rounded_corners table table td   
                                                                                                                       {            border-style: none;  
                                                                                                                                          }   
                                                                                                                                           .style3
        {
            height: 38px;
        }
                                                                                                                                           .rounded_corners
        {
            font-family: Andalus;
        }
        .rounded_corners
        {
            font-family: Andalus;
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
              <tr>
                  <td colspan="3" class="style9">
                      <asp:Panel ID="Panel9" runat="server" BorderStyle="Inset">
                      <fieldset   WIDTH="500PX"    style="background-color: #CCFFFF">
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
                                      <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                                          class="ddl2" CssClass="ddl2" Height="30px" 
                                          onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="230px">
                                      </asp:DropDownList>
                                  </td>
                                  <td width=15% class="style3">
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
                                  <td>
                                      &nbsp;</td>
                                  <td align="left">
                                      <asp:RadioButton ID="Rtoall" runat="server" CssClass="style1" 
                                          oncheckedchanged="Rtoall_CheckedChanged" Text="All Agents" 
                                          AutoPostBack="True" Visible="False" />
                                      <asp:RadioButton ID="rdosingle" runat="server" CssClass="style1" 
                                          oncheckedchanged="rdosingle_CheckedChanged" Text="Single Agents" 
                                          AutoPostBack="True" Checked="True" Visible="False" />
                                  </td>
                                  <td>
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td>
                                      &nbsp;</td>
                                  <td align="right">
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
                                          OnClientClick="return validate();" Text="Submit" CssClass="buttonclass" 
                                          Visible="False" />
                                          
                                    <asp:Button ID="Button2" runat="server" 
                        BorderStyle="Inset" Font-Bold="True" 
                        OnClientClick="return PrintPanel();" Text="Print" CssClass="buttonclass" 
                        BorderColor="#663300" BorderWidth="1px" Height="23px"  />
                                  </td>
                                  

                                  <td>


                                      &nbsp;</td>
                              </tr>
                          </table>
                      
                      
                      
                      </fieldset>
                          <br />
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

                                                                                 <asp:Label ID="Label14" runat="server" Text="Agent Profile" 
                                                                                     style="font-weight: 700; font-family: Andalus; color: #990033;"></asp:Label>


                                                                                 </td>
                                                                         </tr>
                                                                     </table>


                                                                     <asp:DetailsView ID="DetailsView2" runat="server"             
                     AutoGenerateRows="false"      OnDataBound="DetailsView2_DataBound2" 
                     CssClass="gridview1" 
                     onpageindexchanging="DetailsView2_PageIndexChanging" Width="600px" onitemcreated="DetailsView2_ItemCreated">
                                   <Fields>
                                   <asp:TemplateField>  
                                   
                                   <ItemTemplate>





     <table width=100%>


      
     


       <tr align="left">
                         <td  width=35%>
                            <asp:Label ID="Label9" runat="server"  Text="Plant Name"  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label10" Text='<%# Eval("plant_name") %>'  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>

                     </tr>
                     <tr  align="left">
                         <td  width=30%>
                            <asp:Label ID="Label1" runat="server"  Text="Route Name"   Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label4" Text='<%# Eval("Route_Name") %>'  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>

                            
                     </tr>

    
                     <tr align="left">
                         <td  width=35%>
                            <asp:Label ID="Label2" runat="server"  Text="AgentId"  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label7" Text='<%# Eval("Agent_Id") %>'  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>
                           
                     </tr>
                   
                    <tr  align="left">
                         <td  width=30%>
                            <asp:Label ID="Label3" runat="server"  Text="AgentName"     Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label5" Text='<%# Eval("Agent_Name") %>'  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>

                             
                     </tr>
                       
                      <tr  align="left">
                         <td  width=30%>
                            <asp:Label ID="Label6" runat="server"  Text="Agent Photo"   Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl='<%# Eval("Image")%>'
      Width="60px" Height="60px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);"  Font-Bold="True" Font-Names="Andalus" />
                             </td>
                            
                     </tr>
                      <tr  align="left">
                         <td  width=30%>
                            <asp:Label ID="Label12" runat="server"  Text="Address"   Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label13" Text='<%# Eval("Address") %>'  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>

                           
                     </tr>
                      <tr  align="left">
                         <td width=30%>
                            <asp:Label ID="Label22" runat="server"  Text="DoJ Date"  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label23" Text='<%# Eval("JoiningDate") %>'  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>
                            
                     </tr>
                      <tr  align="left">
                         <td  width=30%>
                            <asp:Label ID="Label24" runat="server"  Text="Mobile No"    Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label25" Text='<%# Eval("Mobile") %>'  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>
                     </tr>
                      <tr  align="left">
                         <td  width=30%>
                            <asp:Label ID="Label26" runat="server"  Text="Nominee Name"   Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label27" Text='<%# Eval("GuardianName") %>'  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>
                            
                     </tr>
                      <tr  align="left">
                         <td  width=30%>
                            <asp:Label ID="Label28" runat="server"  Text="AadharCard Proof"   Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl='<%# Eval("Aadharimage")%>'
                Width="60px" Height="60px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);"  Font-Bold="True" Font-Names="Andalus" /> <br />
                   <asp:Label runat="server" ID="Label15" Text='<%# Eval("AadharNo") %>'  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>
                             
                     </tr>
                      <tr  align="left">
                         <td  width=30%>
                            <asp:Label ID="Label30" runat="server"  Text="RatinCard Proof"  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                           <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl='<%# Eval("Rationimage")%>'
                Width="60px" Height="60px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);"  Font-Bold="True" Font-Names="Andalus" /> <br />
                  <asp:Label runat="server" ID="Label16" Text='<%# Eval("RationCartNo") %>'  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>

                     </tr>

                       <tr  align="left">
                         <td  width=30%>
                            <asp:Label ID="Label11" runat="server"  Text="VoterId Proof"  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                           <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl='<%# Eval("voterimage")%>'
                Width="60px" Height="60px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" Font-Bold="True" Font-Names="Andalus" /><br />
                 <asp:Label runat="server" ID="Label17" Text='<%# Eval("VoterId") %>'  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>

                             
                     </tr>


                       <tr  align="left">
                         <td  width=30%>
                            <asp:Label ID="Label29" runat="server"  Text="Pancard Proof" Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                           <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl='<%# Eval("pancardimage")%>'
                Width="60px" Height="60px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" /> <br />
                 <asp:Label runat="server" ID="Label19" Text='<%# Eval("PanCardNo") %>'  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>

                     </tr>


                      <tr  align="left">
                         <td  width=30%>
                            <asp:Label ID="Label31" runat="server"  Text="BankName"  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label33" Text='<%# Eval("BankName") %>' Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>

                           
                     </tr>


                      <tr  align="left">
                         <td width=30%>
                            <asp:Label ID="Label34" runat="server"  Text="BankAccNo"  Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label35" Text='<%# Eval("BankAccNo") %>' Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>

                     </tr>


                       <tr  align="left">
                         <td width=30%>
                            <asp:Label ID="Label36" runat="server"  Text="Ifsc Code" Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label37" Text='<%# Eval("IfscNo") %>' Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>

                            
                     </tr>




                       <tr  align="left">
                         <td  width=30%>
                            <asp:Label ID="Label32" runat="server"  Text="Bank A/c Proof" Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                           <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl='<%# Eval("Accountimage")%>' Font-Bold="True" Font-Names="Andalus"
                Width="60px" Height="60px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                             </td>

                            
                     </tr>

                     <tr  align="left">
                         <td width=30%>
                            <asp:Label ID="Label20" runat="server"  Text="Date Of Birth" Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label38" Text='<%# Eval("Dob") %>' Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>

                            
                     </tr>


                         <tr  align="left">
                         <td width=30%>
                            <asp:Label ID="Label39" runat="server"  Text="Marriage Date" Font-Bold="True" Font-Names="Andalus"></asp:Label>
                            </td>
                         <td>
                             <asp:Label runat="server" ID="Label40" Text='<%# Eval("MarriageDate") %>' Font-Bold="True" Font-Names="Andalus"></asp:Label>
                             </td>

                            
                     </tr>



                 </table>

                 <%-- %>

                                   <table>
                                   <td>
                                       <asp:Label ID="Label4" runat="server" Text="Agent Details"></asp:Label><br />
                                   
                                   </td>
                                   
                                   </table>








                                   <table id="Table1" runat="server" width="400px" cellpadding="0" cellspacing="0">     
                                         <tr align =center>
                                         
                                         <td align="left">
                                             <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

                                         </td>
                                         <td>
                                          <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Image")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                                            </td>                                   
                                         </tr>

                                          <tr height="60"  valign=top>
                                          <td width="200" align="left">   </asp:Label> <br /> </td>
                                 <td>  </td>   


                                      </tr> 


                                      <tr height="30"  valign=top align=center>
                                      <td align="left">  
                                            </td>
                                        <td>   </td>  

                                      </tr> 


                                       <tr height="30"  valign=top>

                                           <td width="200" align="left">AgentName: <br /></td>
                                    <td>  <asp:Label runat="server" ID="Label8" Text='<%# Eval("Agent_Name") %>'></asp:Label></td>     
                                      </tr> 


                                       <tr height="30"  valign=top>
                                          <td width="200" align="left">Route Name: <br /> </td>
                                       <td>    <asp:Label runat="server" ID="Label9" Text='<%# Eval("Route_Name") %>'></asp:Label></td>  

                                      </tr> 



                                      



                                       <tr height="30"  valign=top>
                                        
                                         <td width="200" align="left">Address:<br />         </td>
                                       <td>  <asp:Label runat="server" ID="Label10" Text='<%# Eval("Address") %>'></asp:Label></td>   

                                      </tr> 




                                       <tr height="30"  valign=top>
                                       <td width="200" align="left">DoJDate: <br /></td>
                                      <td>  <asp:Label runat="server" ID="Label14" Text='<%# Eval("JoiningDate") %>'></asp:Label></td>

                                      </tr> 




                                       <tr height="30"  valign=top>
                                        <td width="200" align="left"><left>Mobile:</left><br /></td>
                                        <td>   <asp:Label runat="server" ID="Label15" Text='<%# Eval("Mobile") %>'></asp:Label></td>  

                                      </tr> 




                                       <tr height="30"  valign=top>
                                        <td width="200" align="left">Nominee: <br /> </td> 
                                     <td>   <asp:Label runat="server" ID="Label16" Text='<%# Eval("GuardianName") %>'></asp:Label></td> 

                                      </tr> 



                                       <tr height="60"  valign=top>
                                       
   <td width="33%" align="left">Aadharimage: <br /></td>
    <td><asp:ImageButton ID="ImageButton8" runat="server" ImageUrl='<%# Eval("Aadharimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" /></td> 

                                      </tr> 


                                      
                                       <tr height="60"  valign=top>
                                   
                                   <td width="33%" align="left">Rationimage: <br /> </td>
                                <td>   <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl='<%# Eval("Rationimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" /></td>  

                                      </tr> 

                                    

                                        <tr height="60"  valign=top>
                                   
                                     <td width="33%" align="left">voterimage: <br /> </td>
                                  <td>    <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl='<%# Eval("voterimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" /></td>  

                                      </tr> 


                                        <tr height="60"  valign=top>
                                   

                                     <td width="33%" align="left">pancardimage: <br /> </td>
                                   <td>   <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl='<%# Eval("pancardimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" /></td>      
                                      </tr> 


                                        <tr height="30"  valign=top>
                                   
                                 <td width="33%" align="left"><left> BankName:</left><br /> </td>
                                  <td>   <asp:Label runat="server" ID="Label17" Text='<%# Eval("BankName") %>'></asp:Label></td> 

                                      </tr> 



                                        <tr height="30"  valign=top>
                                   
                                   <td width="33%" align="left">BankAccNo: <br /> </td> 
                                <td>   <asp:Label runat="server" ID="Label19" Text='<%# Eval("BankAccNo") %>'></asp:Label></td> 

                                      </tr> 


                                        <tr height="30"  valign=top>
                                   
                                    <td width="33%" align="left">IfscNo: <br />  </td>
                                <td>    <asp:Label runat="server" ID="Label20" Text='<%# Eval("IfscNo") %>'></asp:Label></td>

                                      </tr> 

                                         <tr height="60"  valign=top>
                                      <td width="33%" align="left">Accountimage: <br /> </td>
                                    <td>  <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl='<%# Eval("Accountimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" /></td> 
                </tr>
                               

                       <%--     <tr height="60" valign=top>
                                 <td width="33%" align="center">Address:<br /> <asp:Label runat="server" ID="Label1" Text='<%# Eval("Address") %>'></asp:Label></td>   
                                    <td width="33%" align="center">DoJDate: <br /> <asp:Label runat="server" ID="Label2" Text='<%# Eval("JoiningDate") %>'></asp:Label></td>     
                                   <td width="33%" align="center">Mobile: <br /> <asp:Label runat="server" ID="Label5" Text='<%# Eval("Mobile") %>'></asp:Label></td>  
                                   <td width="33%" align="center">Nominee: <br />  <asp:Label runat="server" ID="Label6" Text='<%# Eval("GuardianName") %>'></asp:Label></td>       
                          </tr> 

                            <tr height="60" valign=top>
                                 <td width="33%" align="center">Aadharimage: <br /> <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl='<%# Eval("Aadharimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" /></td>   
                                    <td width="33%" align="center">Rationimage: <br /> <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl='<%# Eval("Rationimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" /></td>     
                                   <td width="33%" align="center">voterimage: <br /> <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl='<%# Eval("voterimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" /></td>  
                                   <td width="33%" align="center">pancardimage: <br />  <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl='<%# Eval("pancardimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" /></td>       
                          </tr> 

                            <tr height="60" valign=top>
                                 <td width="33%" align="center">BankName:<br /> <asp:Label runat="server" ID="Label11" Text='<%# Eval("BankName") %>'></asp:Label></td>   
                                    <td width="33%" align="center">BankAccNo: <br /> <asp:Label runat="server" ID="Label12" Text='<%# Eval("BankAccNo") %>'></asp:Label></td>     
                                   <td width="33%" align="center">IfscNo: <br /> <asp:Label runat="server" ID="Label13" Text='<%# Eval("IfscNo") %>'></asp:Label></td>  
                                   <td width="33%" align="center">Accountimage: <br /> <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl='<%# Eval("Accountimage")%>'
                Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" /></td>       
                          </tr> 
                             
                   --%>
                                                     </table>    
                                                     
                                                       
                                                           </ItemTemplate>     

                                                                </asp:TemplateField>  
                                                                      </Fields>
                                                                   </asp:DetailsView>




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
        </asp:UpdatePanel>
     
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

