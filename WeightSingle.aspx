<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WeightSingle.aspx.cs" Inherits="WeightSingle" Title="OnlineMilkTest|Weigher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link id="Link4"  runat="server" rel="shortcut icon" href="~/image/favicon.ico" type="image/x-icon"/>
 <link id="Link3" runat="server" rel="icon" href="~/image/favicon.ico" type="image/ico"/>
<meta name="keywords" content="Milk procurement, milk test, milk collection, milk software, milk analyser, analyzer, milk products software, dairy software, milk ERP, milk accounts software,milk weighing machine, milk weighing scale, milk chilling center software, collection centre software, milk test software, milk procurement software, software for dairy industry, online milk procurement, online milk test, online milk ERP, milk test online, Indian milk software, software in tamilnadu, dairy software in Tamilnadu, milk software in south India, milk reports, milk billing, dairy billing software, FAT, SNF, TS charts, Eko Milk Ultra sales, Eko Milk Ultra Distributor, Eko milk ultra Service, Eko Milk sales, Eko Milk Distributor, Eko milk Service, Milk Stirrer, Milk Producers, Milk products, Dairy Products and Solutions,procurement, collection, milk, farmers, direct, reach, milk tanker, milk plant,milk processing, milk process, milk packaging, milk package" />
    <meta name="description" content="MILK TEST (PROCUREMENT) SOFTWARE | Online & Windows Application" />
    <title>Weigher</title>
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

     <link id="Link1"  rel="Stylesheet" type="text/css" href="SampleStyleSheet1.css" runat="server" />
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
            #Comport
            {
                height: 0px;
                width: 1px;
            }
        </style>

        <script type="text/javascript">
            var message = "Sorry, right-click has been disabled";
            ///////////////////////////////////
            function clickIE() { if (document.all) { (message); return false; } }
            function clickNS(e) {
                if
(document.layers || (document.getElementById && !document.all)) {
                    if (e.which == 2 || e.which == 3) { (message); return false; } 
                } 
            }
            if (document.layers)
            { document.captureEvents(Event.MOUSEDOWN); document.onmousedown = clickNS; }
            else { document.onmouseup = clickNS; document.oncontextmenu = clickIE; }
            document.oncontextmenu = new Function("return false")


            function OpenActiveX() {

                var port = document.getElementById("Comport");
                port.attachEvent('EventDataReceived', EventDataReceived);
                // port.Conopen = 1;
                port.Baudrate = text2.value;
                port.Port = text3.value;               
                port.DataBits = 8;
                port.Parity = 0; //NOPARITY
                port.StopBits = 0; //one stop bit
                port.DataMode = 1;
                port.PortConnecteed = 1;
                if (port.error > 0) //Display errors if found
                    alert(port.ErrorDescription);
            }
            function EventDataReceived(getvalue, getcount) {
                if (getcount == 0) {
                    text1.value = " ";
                    text1.value = text1.value + getvalue;
                    var hiddenControl = '<%= inpHide.ClientID %>';
                    document.getElementById(hiddenControl).value = text1.value;
                }
            }
            
 

</script>

      
        
        
</head>
<body onload="OpenActiveX()"> 
<div id="page-background">
<img src="backround.jpg" width="100%" height="100%" alt="Smile"/>
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
          <p><font class="subheading">&nbsp;&nbsp;Weigher
          </font></p>
          </td>
        </tr>
        
        <tr>
          <td width="100%" class="line" height="1px" colspan="2"></td>
        </tr>
        </table>
    </div>
    <div align="left" class="comobj">
	<object classid="CLSID:6A603FD5-5F5C-4988-996F-4B695E60AE2C" id="Comport"></object> 	
    </div>			
    <div>
     <div class="legendauto" >
     <fieldset class="fontt">
    
     <legend  class="fontt" style=" border:thin 10px gray auto;">Weigher</legend>
    
     
  
                            Port name&nbsp;
								
 
     
     
										<input id="text3" type="text" size="8" maxlength="8" value="<%=portname%>" readonly="readonly" disabled="disabled"/>&nbsp;&nbsp;
                                       
                                      
                                      
                                    Baudrate&nbsp;
								
     
     
										<input id="text2" type="text" size="8" maxlength="8" value="<%=baudrate%>" readonly="readonly" disabled="disabled"/>&nbsp;&nbsp;
                                       
                                      
								 Weight&nbsp;
							
							
             <input id="text1" type="text" size="45"  
             maxlength="5" 
             style="font-size:45px; font-family: Arial, Helvetica, sans-serif; color:Black; width: 150px; height: 90px; font-weight:bold;"/>   
    
										<br />
    <table width="500px">
    <tr>
    <td align="right">
                                       
          
										 <input id="Button1" type="button" value="Load" onclick="OpenActiveX()"; 
                                             
                                             
                                             style="background-color: #6F696F; color: #FFFFFF; "/>
                                       
                                    </td></tr></table>
    
    
   
         
  </fieldset>
     
    </div> 


    <form id="Form1" runat="server">    

   <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
   </asp:ToolkitScriptManager>
 <div class="legselection">
      <fieldset class="fontt">
      <legend class="fontt">Selection</legend>

     <table width="100%">
                                <tr>
                                
								<td width="21%">
								<asp:RadioButton ID="auto" runat="server" Checked="true" Text="Automatic" 
                                        AutoPostBack="True" oncheckedchanged="auto_CheckedChanged" />
								<asp:RadioButton ID="manual" runat="server" Checked="false" Text="Manual" 
                                        AutoPostBack="True" oncheckedchanged="manual_CheckedChanged" />
								
								
								</td>
								
                                        
                                      
                                       
                                      </tr>
                                      
                                      </table>
                                      </fieldset>
                                      </div><br />
    <br />
    <br />
                                      <div class="legselection1">
      <fieldset class="fontt">
      <legend class="fontt">Selection</legend>
                                      <table width="100%">
                                      <tr>
                                      <td width="25%" >
								<asp:RadioButton ID="rd_Cow" runat="server" Checked="True" Text="Cow" 
                                        AutoPostBack="True" oncheckedchanged="rd_Cow_CheckedChanged" />&nbsp;
                                    <asp:RadioButton ID="rd_Buffalo" runat="server" Text="Buffalo" 
                                        AutoPostBack="True" oncheckedchanged="rd_Buffalo_CheckedChanged" />
                                        
                                    &nbsp;
                                        
                                    <asp:TextBox ID="txt_milktype" runat="server" Width="10px" Visible="False"></asp:TextBox>
                                    </td>
                                      </tr>
                                      </table> </fieldset>
                                      </div><br />
      
      
                                      <div class="legmanual" >
      <fieldset class="fontt">
      <legend class="fontt">Manual</legend>
           <table width="100%">
           <tr>
           <td class="altd1">
            <asp:DropDownList ID="ddl_RouteID" runat="server" AutoPostBack="true"  
                    Height="16px" onselectedindexchanged="ddl_RouteID_SelectedIndexChanged" 
                    Visible="False" Width="149px">
                <asp:ListItem>Select</asp:ListItem>
                </asp:DropDownList></td>
            <td class="altd1">
                &nbsp;</td>
           </tr>
           <tr>
           <td class="style1">Route Name</td>
            <td class="altd1">
            <asp:DropDownList ID="txtroutename" runat="server" AutoPostBack="true"  Value="txtRoute_ID"
                    Height="16px" onselectedindexchanged="txtroutename_SelectedIndexChanged" 
                    Width="149px">
                <asp:ListItem>Select</asp:ListItem>
                </asp:DropDownList></td>
           </tr>
           
           <tr>
           
           <td class="style1">Agent ID</td>
             <td class="altd1">
             
								
										<asp:DropDownList ID="txtAgentId" runat="server" AutoPostBack="true"
                                            onselectedindexchanged="txtAgentId_SelectedIndexChanged" Width="149px">
										<asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
             
             </td>
           </tr>
								
                                      <tr>
                                      
                                      <td class="style1">Agent name</td>
                                      <td class="altd1">
										<asp:DropDownList
                                            ID="txtAgentName" runat="server" AutoPostBack="true"
                                            onselectedindexchanged="txtAgentName_SelectedIndexChanged" Width="149px"><asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                      </td>
                                      </tr>
								
                                       <tr>
                                      
                                      <td class="style1">No of Cans</td>
                                      <td class="altd1">
                                      <asp:TextBox ID="txt_Noofcans"  runat="server" Width="143px"></asp:TextBox>
                                      </td>
                                      </tr>
                                      
                                      <tr>
                                     
									
									
										 <td class="style1">Select chart</td>
									
										 <td class="altd1">
										<asp:DropDownList
                                            ID="cmb_ratechart" runat="server" AppendDataBoundItems="True" 
                                            AutoPostBack="True" Width="149px"><asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                     </td>
                                     
                                      </tr>
                                      
									<tr>
										
									
								<td class="style1">Date</td>
							
										<td class="altd1">
										<asp:TextBox ID="TextBox1" runat="server" Enabled="False" Width="143px"></asp:TextBox>
                                        
 </td> 
                                      </tr>
                                      
									<tr>
										
									<td class="style1">Session</td>
								
										<td class="altd1">
										<asp:DropDownList ID="cmb_session" runat="server" AppendDataBoundItems="True" 
                                                AutoPostBack="True" Width="149px">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>AM</asp:ListItem>
                                            <asp:ListItem>PM</asp:ListItem>
                                            </asp:DropDownList>
                                       </td>
                                       
                                      </tr>
									
									<tr>
										<td class="altd1">
                                            <asp:Label ID="Label1" runat="server" Text="Milkkg" style="color: #3399FF"></asp:Label></td>
								
										<td class="altd1">
										<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkCssClass="waterMark" WatermarkText="Type Weight" TargetControlID="txtWeight" runat="server">

</asp:TextBoxWatermarkExtender>
										<asp:TextBox ID="txtWeight" runat="server" Width="120px"></asp:TextBox><em class="fontem"><asp:Label
                                            ID="Label2" runat="server" Text="Kg"></asp:Label></em>
										  </td>
										
                                      </tr>
                                      
									<tr>
										<td class="style1">
                                            <asp:Label ID="Label3" runat="server" Text="Milkltr"></asp:Label></td>
									
										<td class="altd1">
										<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" WatermarkCssClass="waterMark" WatermarkText="Type Ltr" TargetControlID="txt_Milkltr" runat="server">

</asp:TextBoxWatermarkExtender>
										<asp:TextBox ID="txt_Milkltr" runat="server" Width="120px"></asp:TextBox><em class="fontem"><asp:Label
                                            ID="Label4" runat="server" Text="Ltr"></asp:Label></em>
										  </td>
										 
                                      </tr>
                                      <tr>
               <td class="style1">Truck_Id</td>
              
               <td class="altd1">
								
								
										<asp:TextBox ID="txt_TruckID" runat="server" Enabled="False"></asp:TextBox>
                                          </td>
                                     
                                      </tr>
                                      <tr>
                <td class="altd1">
                                      <asp:TextBox ID="txtAgentName1"  runat="server" Width="149px" Visible="False"></asp:TextBox>
                                          </td>
									
										 <td class="altd1">
										     &nbsp;</td>
                                   
                                      </tr>
                                      <tr>
										
										<td class="altd1">
             
             <asp:TextBox ID="txtAgentId1" runat="server" Visible="False" ></asp:TextBox>
             
                                          </td>
                <td class="altd1" align="right">
                 <asp:Button ID="Button2" runat="server" Text="Button" Visible="False" 
                        onclick="Button2_Click2" />
           <input id="inpHide" type="hidden" runat="server" />
										<asp:Button ID="BtnLock" runat="server" Text="Lock" onclick="BtnLock_Click" 
                                                BackColor="#6F696F" Font-Bold="False" ForeColor="White" Width="49px"  OnClientClick="return confirm('Are you sure you want to Lock this Weight?');" />
                                           
             
										 </td> 
                
                       
                    </tr>
                                      </table> 
                                      </fieldset>
                                      </div>
                                      <br />
                                       <center>
                                      <div class="grid">
        <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <mcn:DataPagerGridView ID="gvProducts" runat="server" OnRowDataBound="RowDataBound"
                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" CssClass="datatable"
                    CellPadding="0" BorderWidth="0px" GridLines="None" DataSourceID="WeGrid" 
                    PageSize="5">
                    <Columns>
    <asp:BoundField HeaderText="Agent_id" DataField="Agent_id" SortExpression="Agent_id"
                            HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
                        </asp:BoundField>    
        
        
        
        <asp:BoundField DataField="Milk_kg" HeaderText="Milk_kg" SortExpression="Milk_kg" 
                            ReadOnly="True" />        
        <asp:BoundField DataField="Milk_ltr" HeaderText="Milk_ltr" SortExpression="Milk_ltr" 
                            ReadOnly="True" />
        <asp:BoundField DataField="NoofCans" HeaderText="NoofCans" SortExpression="NoofCans" 
                            ReadOnly="True" />
        <asp:BoundField DataField="Route_id" HeaderText="Route_id" SortExpression="Route_id" />
        <asp:BoundField DataField="Prdate" HeaderText="Prdate" SortExpression="Prdate" 
                            ReadOnly="True" />         
        <asp:BoundField DataField="Sessions" HeaderText="Sessions" SortExpression="Sessions" />
        <asp:BoundField DataField="Plant_Code" HeaderText="Plant_Code" SortExpression="Plant_Code" />
        <asp:BoundField DataField="Company_Code" HeaderText="Company_Code" 
                            SortExpression="Company_Code" />
         
            
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
                                        
        <br />
        <br />
         <asp:SqlDataSource ID="WeGrid" runat="server" 
          ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>"                     
          SelectCommand="
SELECT [Agent_id],CAST([Milk_kg] AS DECIMAL(18,2)) AS [Milk_kg],CAST([Milk_ltr] AS DECIMAL(18,2)) AS [Milk_ltr],CAST([NoofCans] AS DECIMAL(18,2)) AS [NoofCans],[Route_id],CONVERT(VARCHAR(10),[Prdate],103) AS Prdate,[Sessions],[Plant_Code],[Company_Code] FROM PROCUREMENT WHERE ([Prdate]=@Prdate) AND ([Sessions]=@Sessions) AND ([Route_id]=@Route_id) AND ([Plant_Code]=@Plant_Code) AND ([Company_Code]=@Company_code)  ORDER BY [Agent_id]
">
             <SelectParameters>
                 <asp:ControlParameter ControlID="TextBox1" Name="Prdate" PropertyName="Text" />
                 <asp:ControlParameter ControlID="cmb_session" Name="Sessions" 
                     PropertyName="SelectedValue" />
                 <asp:ControlParameter ControlID="ddl_RouteID" Name="Route_id" 
                     PropertyName="SelectedValue" />
                 <asp:SessionParameter DefaultValue="Plant_Code" Name="Plant_Code" 
                     SessionField="Plant_Code" />
                 <asp:SessionParameter DefaultValue="Company_code" Name="Company_code" 
                     SessionField="Company_code" />
             </SelectParameters>
                </asp:SqlDataSource>
       </center>
                                      <%--<div class="GridviewDiv">
                                      <center>
                                      <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                                AllowSorting="True" Font-Names="Verdana" Font-Size="10pt" 
                                                onselectedindexchanged="GridView1_SelectedIndexChanged" Width="540px">
                 <HeaderStyle BackColor="#006699" ForeColor="White" />
                                            </asp:GridView>
                                            </center>
                                      </div>--%>
     
     
          
       

<uc1:uscMsgBox ID="uscMsgBox1" runat="server" /> 

</form>

                                      </div>
    
    </div>
      
    <br />
          <div align="center" style="font-family: 'Times New Roman', Times, serif; font-size: 13px;
                    font-weight: inherit; text-transform: inherit; color: #FFFFFF; font-variant: normal;">
                   <p style="color:White;font-family:Verdana,Arial,Helvetica,sans-serif;font-size: 9pt;font-weight: normal;font-style: normal;">
                       Best viewed in Internet Explorer 7.0 and above | Best view 1024 x 768 screen resolution</p>
                       <a href="http://www.microsoft.com/en-us/download/details.aspx?id=2" title="" target="_blank" style="text-shadow: white;
                        color: White; text-decoration: none;">
                        Internet Explorer 7</a>
                        <br />
                    <a href="http://10solution.com" title="10solution In." target="_blank" style="text-shadow: white; background-color:White;
                        color: White; text-decoration: none;">
                        <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/Image/logo.png" />--%></a>
                </div>

    
     </div>
     
    
         </div>
</body>
</html>
       
        
          
        
        
										 

