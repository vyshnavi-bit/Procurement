<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logoutnew.aspx.cs" Inherits="Logoutnew"  Title="OnlineMilkTest|Logout"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta name="keywords" content="Milk procurement, milk test, milk collection, milk software, milk analyser, analyzer, milk products software, dairy software, milk ERP, milk accounts software,milk weighing machine, milk weighing scale, milk chilling center software, collection centre software, milk test software, milk procurement software, software for dairy industry, online milk procurement, online milk test, online milk ERP, milk test online, Indian milk software, software in tamilnadu, dairy software in Tamilnadu, milk software in south India, milk reports, milk billing, dairy billing software, FAT, SNF, TS charts, Eko Milk Ultra sales, Eko Milk Ultra Distributor, Eko milk ultra Service, Eko Milk sales, Eko Milk Distributor, Eko milk Service, Milk Stirrer, Milk Producers, Milk products, Dairy Products and Solutions,procurement, collection, milk, farmers, direct, reach, milk tanker, milk plant,milk processing, milk process, milk packaging, milk package" />
<meta name="description" content="MILK TEST (PROCUREMENT) SOFTWARE | Online & Windows Application" />
<link id="Link1"  rel="Stylesheet" type="text/css" href="SampleStyleSheet1.css" runat="server" media="screen" />
    
<style type="text/css">
/* Remove margins from the 'html' and 'body' tags, and ensure the page takes up full screen height */
html, body {height:100%; margin:0; padding:0;}
/* Set the position and dimensions of the background image. */
#page-background {position:fixed; top:0; left:0; width:100%; height:100%;}
/* Specify the position and layering for the content that needs to appear in front of the background image. Must have a higher z-index value than the background image. Also add some padding to compensate for removing the margin from the 'html' and 'body' tags. */
#content {position:relative; z-index:1;}
</style>
    <title>Logout</title>
</head>
<body>
    <form id="form1" runat="server" style="background-color: #FFCC66">
    <%--<div id="page-background"><img src="backround.jpg" width="100%" height="100%" alt="Smile"/>--%></div>
     <div id="content">
     <div id="everthing">
     
     <table  width="100%">
        <tr>
          <td   width="100%">
          <asp:Label ID="title" Text="ONLINE MILK TEST" runat="server"  CssClass="Titleheader" Visible="false"></asp:Label>
                        <asp:Image ID="Image2" runat="server"  ImageUrl="~/Image/LL.png" 
                  Height="100px" Width="900px" />                    
          </td>          
        </tr>       
          </table> 
          <div id="main">
    <div>
    <h1 class="subheading">
        &nbsp;&nbsp;&nbsp; </h1>
        <p class="subheading">
            LOGOUT</p>
    <p class="fontt" style=" margin-left:100px;">
        
        you have been logged out of the system. To log in, please return to the 
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="LoginDefault.aspx">Login Page</asp:LinkButton> <br />
         <br />
         <br />
            </p>
    </div>
         </div>
        
         
        
      
        
        
        <div align="center" class="foo" >
   
       <a href="home.aspx" class="fotmenu">Home</a><em  style="color:White;">|</em>       
               
       <a href="Agent.aspx" class="fotmenu">Agent</a> <em  style="color:White;">|</em>    
            
      <a onclick="window.open ('WeightSingle.aspx', '_blank', 'width=1280, height=1000, scrollbars=yes, resizable=yes, location=no, status=no, menubar=no, toolbar=no');" class="fotmenu" >Weigher</a><em  style="color:White;">|</em>      
      <a onclick="window.open ('AnalizerSingle.aspx', '_blank', 'width=1280, height=1000, scrollbars=yes, resizable=yes, location=no, status=no, menubar=no, toolbar=no');" class="fotmenu">Milk Analizer</a> <em  style="color:White;">|</em>  
       <a href="RateChartViewer.aspx" class="fotmenu" >Ratechart</a> <em  style="color:White;">|</em>     
      <a href="Contact us.aspx" class="fotmenu" >Contact</a>            
    </div>
    <br />
          <div align="center" style="font-family: 'Times New Roman', Times, serif; font-size: 13px;
                    font-weight: inherit; text-transform: inherit; color: #FFFFFF; font-variant: normal;">
                   <p style="color:White;font-family:Verdana,Arial,Helvetica,sans-serif;font-size: 9pt;font-weight: normal;font-style: normal;">
                       Best viewed in <a href="http://www.microsoft.com/en-us/download/details.aspx?id=2" title="" target="_blank" style="text-shadow: white;
                        color: White; text-decoration: none;">Internet Explorer 7.0</a> and above | Best 
                       screen resolution 1024 x 768</p>
                       
                      
                        <br />
                        <!--
                   <a href="http://10solution.com" title="10solution In." target="_blank" style="background:White;   text-decoration: none;">
                      
                       <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/logo.png" /></a>-->
                </div>
 
        
            
</div>
   </div>
    
    </form>
</body>
</html>
