<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnalizerSingle.aspx.cs" Inherits="AnalizerSingle" Title="OnlineMilkTest|Analyzer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link id="Link4"  runat="server" rel="shortcut icon" href="~/image/favicon.ico" type="image/x-icon"/>
    <link id="Link3" runat="server" rel="icon" href="~/Image/favicon.ico" type="image/ico"/>
    <meta name="keywords" content="Milk procurement, milk test, milk collection, milk software, milk analyser, analyzer, milk products software, dairy software, milk ERP, milk accounts software,milk weighing machine, milk weighing scale, milk chilling center software, collection centre software, milk test software, milk procurement software, software for dairy industry, online milk procurement, online milk test, online milk ERP, milk test online, Indian milk software, software in tamilnadu, dairy software in Tamilnadu, milk software in south India, milk reports, milk billing, dairy billing software, FAT, SNF, TS charts, Eko Milk Ultra sales, Eko Milk Ultra Distributor, Eko milk ultra Service, Eko Milk sales, Eko Milk Distributor, Eko milk Service, Milk Stirrer, Milk Producers, Milk products, Dairy Products and Solutions,procurement, collection, milk, farmers, direct, reach, milk tanker, milk plant,milk processing, milk process, milk packaging, milk package" />
    <meta name="description" content="MILK TEST (PROCUREMENT) SOFTWARE | Online & Windows Application" />
    <title>Analyzer</title>
   
    <style type="text/css">
/* Remove margins from the 'html' and 'body' tags, and ensure the page takes up full screen height */
html, body {height:100%; margin:0; padding:0;}
/* Set the position and dimensions of the background image. */
#page-background {position:fixed; top:0; left:0; width:100%; height:100%;}
/* Specify the position and layering for the content that needs to appear in front of the background image. Must have a higher z-index value than the background image. Also add some padding to compensate for removing the margin from the 'html' and 'body' tags. */
#content {position:relative; z-index:1; padding:10px;}
.hiddendiv {display:none;}
</style>

<link id="Link1"  rel="Stylesheet" type="text/css" href="SampleStyleSheet1.css" runat="server" media="screen" />
 <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />
<style type="text/css">
.GridviewDiv {font-size: 100%; font-family:  Verdana,Arial,Helvetica,sans-serif;
    font-size: 8pt;
    font-weight: normal;
    font-style: normal;
    color: Black; }
Table.Gridview{border:solid 1px #6699FF;}
.Gridview th{color:#FFFFFF;border-right-color:#abb079;border-bottom-color:#abb079;padding:0.5em 0.5em 0.5em 0.5em;text-align:center}  
.Gridview td{border-bottom-color:#f0f2da;border-right-color:#f0f2da;
             padding:0.5em 0.5em 0.5em 0.5em;}
.Gridview tr{color: Black; background-color: White; text-align: center;}

.highlight {text-decoration: none;color:black;background:yellow;}
</style>

<!-- The above code doesn't work in Internet Explorer 6. To address this, we use a conditional comment to specify an alternative style sheet for IE 6 -->
<!--[if IE 6]>
<style type="text/css">
html {overflow-y:hidden;}
body {overflow-y:auto;}
#page-background {position:absolute; z-index:-1;}
#content {position:static;padding:10px;}
    #text2
    {
        width: 60px;
    }
    .style1
    {
        font-family: Verdana,Arial,Helvetica,sans-serif;
        font-size: 9pt;
        font-weight: normal;
        font-style: normal;
        text-decoration: none;
        word-spacing: normal;
        letter-spacing: normal;
        text-transform: none;
        text-decoration: none;
        BACKGROUND: none;
        width: 12%;
    }
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

function OpenActiveX() {
    var port = document.getElementById("Comport");
    port.attachEvent('EventDataReceived1', EventDataReceived);
    
    port.Port = text2.value;
    port.Baudrate = text3.value;
    port.C = text4.value;
    port.p = text5.value;
    port.S = text6.value;
    port.D = text7.value;
    port.A = text8.value;
    port.DataBits = 8;
    port.Parity = 0; //NOPARITY
    port.StopBits = 0; //one stop bit
    port.DataAnalizerMode = 1;
    port.AnalizerPortConnecteed = 1;

    if (port.error > 0) //Display errors if found
        alert(port.ErrorDescription);

    var port2 = document.getElementById("Comport");
    port2.attachEvent('EventDataReceived2', EventDataReceived2);
    port2.Port2 = "COM1";
    port2.Baudrate2 = "1200";
    port2.DataBits2 = 8;
    port2.Parity2 = 0; //NOPARITY
    port2.StopBits2 = 0; //one stop bit
    port2.DataAnalizerMode2 = 1;
    port2.AnalizerPortConnecteed2 = 1;
    if (port2.error > 0) //Display errors if found
        alert(port2.ErrorDescription);

    var port3 = document.getElementById("Comport");
    port3.attachEvent('EventDataReceived3', EventDataReceived3);
    port3.Port3 = "COM3";
    port3.Baudrate3 = "1200";
    port3.DataBits3 = 8;
    port3.Parity3 = 0; //NOPARITY
    port3.StopBits3 = 0; //one stop bit
    port3.DataAnalizerMode3 = 1;
    port3.AnalizerPortConnecteed3 = 1;
    if (port3.error > 0) //Display errors if found
        alert(port3.ErrorDescription);

}

function EventDataReceived3(getvalue, getcount) {
    if (getcount == 0) {
        //text1.value=" ";
        text1.value = getvalue + text1.value;
        var hiddenControl = '<%= inpHide.ClientID %>';
        document.getElementById(hiddenControl).value = text1.value;
    }
}


function EventDataReceived2(getvalue, getcount) {
    if (getcount == 0) {
        //text1.value=" ";
        valueA2.value = getvalue + valueA2.value;
        var hiddenControl = '<%= inpHide.ClientID %>';
        document.getElementById(hiddenControl).value = valueA2.value;
    }
}


function EventDataReceived(getvalue, getcount) {
    if (getcount == 0) {
        //text1.value=" ";
        valueA3.value = getvalue + valueA3.value;
        var hiddenControl = '<%= inpHide.ClientID %>';
        document.getElementById(hiddenControl).value = valueA3.value;
    }
}

function text1_onclick() {

}

</script>
    
</head>

<body onload="OpenActiveX()">
<div id="page-background"><img src="backround.jpg" width="100%" height="100%" alt="Smile"/>

</div>
     <div id="content">
 <div id="everthing">

 
<div style="width:100%;">
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
         
         <td class="loginDisplay" width="100%">
                                            
                    
                          <asp:Label ID="title" Text="ONLINE MILK TEST" runat="server"  CssClass="Titleheader" Visible="false"></asp:Label>
                           [<a href="home.aspx" id="A1">Home</a>]
                           [<a href="Logout.aspx" id="A2">Logout</a>]                   
                        <asp:Image ID="Image2" runat="server"  ImageUrl="~/Image/L.png" Height="100px" Width="900px" />                  
                          
                    </td>
        </tr>
        
          </table>
          </div>         
            
     
<div id="main">
<div style="width:100%;">

<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
          <td width="100%" colspan="2">
          <p style="line-height: 150%"><font class="subheading">&nbsp;&nbsp;Analyzer
          </font></p>
          </td>
        </tr>
        <tr>
          <td width="100%" height="3px" colspan="2">
          
          </td>
        </tr>
        <tr>
          <td width="100%" class="line" height="1px" colspan="2"></td>
        </tr>
        </table>
          </div>   
 <div align="left" class="comobj">
            <object classid="CLSID:6A603FD5-5F5C-4988-996F-4B695E60AE2C" id="Comport" ></object> 
    </div> 
 <div class="legendAnalyauto" >
 <table border="0" cellpadding="0" cellspacing="1" width="100%"> 
        <tr>
        
          <td width="33%">
         <div class="legendautoAnaly" >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">Analyzer_1</legend>
<table id="pname1" width="100%">
    <tr>    
     <td width="25px">    
     Port    
    </td>
    <td width="20px">    
     <input id="text2" type="text" size="8" maxlength="5" value="<%=portname%>" readonly="readonly" disabled="disabled"/>&nbsp;&nbsp;
    </td>
     <td width="25px">    
    Baudrate   
    </td>
    <td width="20px">     
     <input id="text3" type="text" size="8" maxlength="5" value="<%=baudrate%>" readonly="readonly" disabled="disabled"/>&nbsp;&nbsp;
    </td>    
     </tr>
    </table> 
    <table id="pvalue1" width="100%">   
    <tr>
    <td width="100%">
    <div align="left" >
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Value
    &nbsp;&nbsp;&nbsp;&nbsp;<input id="text1" type="text" size="15"              
             style="font-size:20px; font-family: Arial, Helvetica, sans-serif; color:Black; width: 90%; height: 60px; font-weight:bold;" onclick="return text1_onclick()" />      
    </div>
    </td>
    </tr>
    </table>      
   
     </fieldset>
   </div>    
          </td>
          <td width="33%">          
           <div class="legendautoAnaly" >
     <fieldset class="fontt">    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">Analyzer_2</legend>
 <table id="pnameA2" width="100px">
    <tr>
    <td  width="25px">    
     Port    
    </td>
    <td  width="20px"> 
     <input id="A2portname" type="text" size="8" maxlength="5" value="COM1" readonly="readonly" disabled="disabled"/>&nbsp;&nbsp;
    </td>
    <td  width="25px">    
     Baudrate    
    </td>
    <td width="20px">    
     <input id="A2baudrate" type="text" size="8" maxlength="5" value="1200" readonly="readonly" disabled="disabled"/>&nbsp;&nbsp;
    </td>
     </tr>
    </table> 
    <table id="pvalueA2" width="100%">   
    <tr>
    <td width="100%">
    <div align="left" >
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Value
    &nbsp;&nbsp;&nbsp;&nbsp;<input id="valueA2" type="text" size="15"              
             style="font-size:20px; font-family: Arial, Helvetica, sans-serif; color: Black; width: 90%; height: 60px;"/>         
    
    </div>
    </td>
    </tr>
    </table>      
   
     </fieldset>
   </div> 
          </td>
          <td width="33%">
          
           <div class="legendautoAnaly" >
     <fieldset class="fontt">
    
     <legend  style=" border:thin 10px gray auto;" class="fontAnaly">Analyzer_3</legend>
 <table id="pnameA3" width="100px">
    <tr>
    <td  width="25px">    
     Port   
    </td>
    <td  width="20px">     
     <input id="A3portname" type="text" size="8" maxlength="5" value="COM88" readonly="readonly" disabled="disabled"/>&nbsp;&nbsp;
    </td>
    <td  width="25px">    
     Baudrate    
    </td>
    <td width="20px">     
     <input id="A3baudrate" type="text" size="8" maxlength="5" value="1200" readonly="readonly" disabled="disabled"/>&nbsp;&nbsp;
    </td>
     </tr>
    </table> 
    <table id="pvalueA3" width="100%">   
    <tr>
    <td width="100%">
    <div align="left" >
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Value   
     &nbsp;&nbsp;&nbsp;&nbsp;<input id="valueA3" type="text" size="15"              
             style="font-size:20px; font-family: Arial, Helvetica, sans-serif;font-style:normal;color: Black; width: 90%; height: 60px;"/>           
    </div>
    </td>
    </tr>
    </table>  
     </fieldset>
   </div> 
          </td>
        </tr>
        
        
        </table>
    </div>   

   <div align="center" class="hiddendiv">
   
      <table >
    <tr >
   <td>
   <div align="center">
       <input id="text4"  type="text"  size="3" value="<%=companycode%>" readonly ="readonly" disabled="disabled"  />
       <input id="text5" type="text"  size="5" value="<%=plantcode%>" readonly ="readonly" disabled="disabled" />
        <input id="text6" type="text"  size="5" value="<%=sess%>" readonly ="readonly" disabled="disabled" />
       <input id="text7" type="text"  size="7" value="<%=cudate%>" readonly ="readonly" disabled="disabled" />
        <input id="text8" type="text"  size="5" value="<%=plantcode%>" readonly ="readonly" disabled="disabled" />
        </div>
       </td>
    <td align="right">     
     <input id="Button1" visible="false" type="button" value="Load" onclick="OpenActiveX()";               
            style="background-color: #6F696F; color: #FFFFFF;  width:55px; " /> 
            </td>
            </tr>
            </table>
           
     </div>

      <form id="form2" runat="server">
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
      </asp:ToolkitScriptManager>
	   
      <br />
      <div align="center" class="hiddendiv">
      <div class="legselectionsession"  >
      <fieldset class="fontt">
      <legend class="fontt" >Selection</legend>
      <table width="100%">
                                <tr>
                              
								<td >
								<asp:RadioButton ID="auto" runat="server" Checked="true" Text="Automatic" 
                                        AutoPostBack="True" oncheckedchanged="auto_CheckedChanged" />
								<asp:RadioButton ID="manual" runat="server" Checked="false" Text="Manual" 
                                        AutoPostBack="True" oncheckedchanged="manual_CheckedChanged" />								
								</td>								
                                      </tr>                                      
                                      </table>                                     
      </fieldset>
      </div>
     
      <div class="legmanual">
      <fieldset class="fontt">
      <legend class="fontt">Manual</legend>
       <table width="100%" id="table3"  >
       <tr>
       
       <td class="style1">Sample No</td>
       
       <td class="altd1">
           <asp:DropDownList ID="cmb_sampleno" runat="server"  Width="111px" 
               Height="20px" >
        <asp:ListItem Value="0">--Select ChartType--</asp:ListItem>
        
    </asp:DropDownList>  </td>
     
       
       </tr>
       <tr>
     
       <td class="style1">Fat</td>
      
       <td  class="altd1"><asp:TextBox ID="txt_fat" runat="server"   Height="16px" 
               Width="155px" ></asp:TextBox>
           </td>
      
       </tr>
       <tr>
       
       <td class="style1">Snf</td>
    
       <td  class="altd1">
       <asp:TextBox ID="txt_snf" runat="server"   Height="16px" 
               Width="155px" ontextchanged="txt_snf_TextChanged" ></asp:TextBox>
           </td>
      
       </tr>
      
       
       <tr>
      
       <td class="style1">Date</td>
      
       <td  class="altd1">
       <asp:TextBox ID="dtp_DateTime" runat="server"   Height="16px" 
               Width="155px" Enabled="False" /> </td>
     
       </tr>
          
          <tr>
       
       <td class="style1">Session</td>
      
       <td  class="altd1">
       <asp:DropDownList ID="cmb_session" runat="server" AppendDataBoundItems="True" 
                                                AutoPostBack="True" Width="46px" 
               Height="16px">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>AM</asp:ListItem>
                                            <asp:ListItem>PM</asp:ListItem>
                                            </asp:DropDownList> </td>
      
       </tr>
       
        <tr>
      
       <td class="altd1"></td>
    
       <td  class="altd1"><asp:TextBox ID="txt_clr" runat="server"   Height="16px" 
               Width="155px" Visible="False" />  </td>
      
       </tr>
       
       
       
       
       
       <tr>
     
       <td class="altd1"></td>
      
       <td  class="altd1" align="right">
            <asp:Button ID="BtnLock" runat="server" Text="Lock" onclick="BtnLock_Click" 
                BackColor="#6F696F" Width="55px" Font-Bold="False" ForeColor="White"  
                OnClientClick="return confirm('Are you sure you want to Lock this Values?');" 
                Height="25px" />
            <input id="inpHide" type="hidden" runat="server" />
            </td>
      
       </tr>
      </table>
      </fieldset>
      </div>
      </div>
      <br />    
      <center>
      <div class="grid">
        <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <mcn:DataPagerGridView ID="gvProducts" runat="server" OnRowDataBound="RowDataBound"
                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" CssClass="datatable"
                    CellPadding="0" BorderWidth="0px" GridLines="None" DataSourceID="AnGrid" 
                    PageSize="5">
                     <Columns>
    <asp:BoundField HeaderText="SamplenID" DataField="Sampleno" SortExpression="Sampleno"
                            HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
                            <HeaderStyle CssClass="first" />
                            <ItemStyle CssClass="first" />
                        </asp:BoundField>
   
      <asp:BoundField DataField="Fat" HeaderText="Fat" SortExpression="Fat" />
      <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" />       
      <asp:BoundField DataField="Prdate" HeaderText="Prdate" SortExpression="Prdate" />        
       <asp:BoundField DataField="Sessions" HeaderText="Sessions" SortExpression="Sessions" />
       <asp:BoundField DataField="Plant_Code" HeaderText="Plant_Code" SortExpression="Plant_Code" />
       <asp:BoundField DataField="Company_Code" HeaderText="Company_Code" SortExpression="Company_Code"/>
            </Columns>
 <PagerSettings Visible="False" />
                    <RowStyle CssClass="row" />
                </mcn:DataPagerGridView>
               
               
               
<div class="pager">
                    <asp:DataPager ID="pager" runat="server" PageSize="8" PagedControlID="gvProducts">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                ShowLastPageButton="false" ShowNextPageButton="false" />
                            <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                NextPreviousButtonCssClass="command" />
                            <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="›"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                ShowLastPageButton="true" ShowNextPageButton="true" />
                        </Fields>
                    </asp:DataPager>
                </div>
                </ContentTemplate>
        </asp:UpdatePanel>
        </div>
         </center>
        <br />
        <br />
         <asp:SqlDataSource ID="AnGrid" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>" 
                    
          
          
          SelectCommand="SELECT [Sampleno],[Fat],[Snf],CONVERT(VARCHAR(10),[Prdate],103) AS prdate,[Sessions],[Plant_Code],[Company_Code] FROM PROCUREMENT WHERE ([Prdate]=@Prdate) AND ([Sessions]=@Sessions) AND ([Plant_Code]=@Plant_Code) AND ([Company_Code]=@Company_code)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="dtp_DateTime" Name="Prdate" 
                            PropertyName="Text" />
                        <asp:ControlParameter ControlID="cmb_session" 
                            Name="Sessions" PropertyName="SelectedValue" />
                        <asp:SessionParameter DefaultValue="Plant_Code" Name="Plant_Code" 
                            SessionField="Plant_Code" />
                        <asp:SessionParameter DefaultValue="Company_code" Name="Company_code" 
                            SessionField="Company_code" />
                    </SelectParameters>
                </asp:SqlDataSource>   
      
     
   <%-- <uc1:uscMsgBox ID="uscMsgBox1" runat="server" /> --%>
     
     
</form>

   
       
</div>
<%-- <div align="center" class="foo" >
   
       <a href="homet.aspx" class="fotmenu">Home</a><em  style="color:White;">|</em>       
               
       <a href="Agent.aspx" class="fotmenu">Agent</a> <em  style="color:White;">|</em>    
       <a href="WeightSingle.aspx" class="fotmenu" >Weigher</a> <em  style="color:White;">|</em>       
      <a href="AnalizerSingle.aspx" class="fotmenu" >Milk Analizer</a><em  style="color:White;">|</em>      
       <a href="RateChartViewer.aspx" class="fotmenu" >Ratechart</a> <em  style="color:White;">|</em>     
      <a href="Contact us.aspx" class="fotmenu" >Contact</a>            
    </div>--%>
    <br />
</div>
</div>
 
</body>
</html>
     
     
     
     
     
     
     
     
          
          
          
          
















































                
         
       
        
          
        
        
										 

        
       
         
       
         

