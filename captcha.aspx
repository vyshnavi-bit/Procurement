<%@ Page Language="C#" AutoEventWireup="true" CodeFile="captcha.aspx.cs" Inherits="captcha" Title="OnlineMilkTest|Captcha" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link id="Link4"  runat="server" rel="shortcut icon" href="~/image/favicon.ico" type="image/x-icon"/>
    <link id="Link3" runat="server" rel="icon" href="~/image/favicon.ico" type="image/ico"/>
<meta name="keywords" content="Milk procurement, milk test, milk collection, milk software, milk analyser, analyzer, milk products software, dairy software, milk ERP, milk accounts software,milk weighing machine, milk weighing scale, milk chilling center software, collection centre software, milk test software, milk procurement software, software for dairy industry, online milk procurement, online milk test, online milk ERP, milk test online, Indian milk software, software in tamilnadu, dairy software in Tamilnadu, milk software in south India, milk reports, milk billing, dairy billing software, FAT, SNF, TS charts, Eko Milk Ultra sales, Eko Milk Ultra Distributor, Eko milk ultra Service, Eko Milk sales, Eko Milk Distributor, Eko milk Service, Milk Stirrer, Milk Producers, Milk products, Dairy Products and Solutions,procurement, collection, milk, farmers, direct, reach, milk tanker, milk plant,milk processing, milk process, milk packaging, milk package" />
<meta name="description" content="MILK TEST (PROCUREMENT) SOFTWARE | Online & Windows Application" />
 <link id="Link1"  rel="Stylesheet" type="text/css" href="SampleStyleSheet1.css" runat="server" />
     
    <title>LOGIN VERIFICATION</title>
    <style type="text/css">
/* Remove margins from the 'html' and 'body' tags, and ensure the page takes up full screen height */
html, body {height:100%; margin:0; padding:0;}
/* Set the position and dimensions of the background image. */
#page-background {position:fixed; top:0; left:0; width:100%; height:100%;}
/* Specify the position and layering for the content that needs to appear in front of the background image. Must have a higher z-index value than the background image. Also add some padding to compensate for removing the margin from the 'html' and 'body' tags. */
#content {position:relative; z-index:1; padding:10px;}
</style>
<!-- The above code doesn't work in Internet Explorer 6. To address this, we use a conditional comment to specify an alternative style sheet for IE 6 -->
<!--[if IE 6]>
<style type="text/css">
html {overflow-y:hidden;}
body {overflow-y:auto;}
#page-background {position:absolute; z-index:-1;}
#content {position:static;padding:10px;}
</style>
<![endif]-->
     <script type="text/javascript">
var message="Sorry, right-click has been disabled";
///////////////////////////////////
function clickIE() {if (document.all) {(message);return false;}}
function clickNS(e) {if
(document.layers||(document.getElementById&&!document.all)) {
if (e.which==2||e.which==3) {(message);return false;}}}
if (document.layers)
{document.captureEvents(Event.MOUSEDOWN);document.onmousedown=clickNS;}
else{document.onmouseup=clickNS;document.oncontextmenu=clickIE;}
document.oncontextmenu=new Function("return false")
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page-background"><img src="backround.jpg" width="100%" height="100%" alt=""/></div>
     <div id="content">
     <div id="everthing">
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
         </asp:ToolkitScriptManager>
   <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
          <td >
         <img src="Image/L.png" alt="" />
          </td>
          <td class="loginDisplay" width="15%">
            </td>
        </tr>
        
          </table>
          
           <div id="main" style="width:100%;">
          <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
          <td width="100%" colspan="2"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;LOGIN VERIFICATION</p>
            </td>
        </tr>
        <tr>
          <td width="100%" height="3px" colspan="2"></td>
        </tr>
        <tr>
          <td width="100%" class="line" height="1px" colspan="2"></td>
        </tr>
        </table>
  
    <center>
    <div class="fontt">
    <img height="30" alt="" src="Turing.aspx" align="middle" width="80"/> <br /><br />
        
        <asp:TextBox ID="txtTuring" runat="server"></asp:TextBox><br /><br />
        
        <asp:Button ID="Button1" runat="server" Text="Verify" onclick="Button1_Click" 
            BackColor="#6F696F" Font-Bold="False" ForeColor="White" Width="49px" />
        <br />
        <br />
        <asp:Label  runat="server" ID="capthast"  Text="Enter the above code"></asp:Label>
        </div>
        </center>
    </div>
    <%--<div align="center" class="foo" >
   
       <a href="homet.aspx" class="fotmenu">Home</a><em  style="color:White;">|</em>       
               
       <a href="Agent.aspx" class="fotmenu">Agent</a> <em  style="color:White;">|</em>    
       <a href="WeightSingle.aspx" class="fotmenu" >Weigher</a> <em  style="color:White;">|</em>       
      <a href="AnalizerSingle.aspx" class="fotmenu" >Milk Analizer</a><em  style="color:White;">|</em>      
       <a href="RateChartViewer.aspx" class="fotmenu" >Ratechart</a> <em  style="color:White;">|</em>     
      <a href="Contact us.aspx" class="fotmenu" >Contact</a>            
    </div>
    <br />--%> <br /><br />
          <div align="center" style="font-family: 'Times New Roman', Times, serif; font-size: 13px; font-weight: inherit; text-transform: inherit; color:#FFFFFF; font-variant: normal;"> 
<p style="color:White;font-family:Verdana,Arial,Helvetica,sans-serif;font-size: 9pt;font-weight: normal;font-style: normal;">
                       Best viewed in Internet Explorer 7.0 and above | Best screen resolution 1024 x 768 </p>
        <%--<a href="http://10solution.com" title="10solution In." target="_blank" style="text-shadow:white; color: White; text-decoration:none;"><img src="Image/logo.png" alt="10solution" /></a>--%>
         </div>
    </div>
  <%--  </div>      <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />--%>
    </form>
</body>
</html>
